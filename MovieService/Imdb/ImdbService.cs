using IMDbApiLib.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using MovieService.Entities;
using MovieService.Repositories;
using MovieService.Imdb;
using System;

namespace Imdb
{
    public class ImdbService : IDisposable
    {
        private CustomHttpClient webClient;
        private MovieTypeRepository movieTypeRepository;
        private GenreRepository genreRepository;
        private CountryRepository countryRepository;

        public ImdbService(MovieTypeRepository movieTypeRepo, GenreRepository genreRepo, CountryRepository countryRepo, CustomHttpClient webClient) { 
            movieTypeRepository = movieTypeRepo; 
            genreRepository = genreRepo;
            countryRepository = countryRepo;
            this.webClient = webClient;
        }

        public List<SearchOption> SearchMovie(string searchOption)
        {
            if (string.IsNullOrEmpty(searchOption))
                return null;

            var json = webClient.GetJson(string.Format(ImdbApi.searchRequest, ImdbApi.apiKey, searchOption));
            webClient.Dispose();
            Console.WriteLine(json);
            var obj = JsonConvert.DeserializeObject<SearchData>(json);
            var result = new List<SearchOption>(obj.Results.Count);
            foreach (var item in obj.Results)
                result.Add(new SearchOption(item.Id, item.Title));

            return result;
        }

        private void MapMovieGenres(List<KeyValueItem> genreList, Movie movie)
        {
            var genreNames = new List<string>(genreList.Count);
            foreach (var genre in genreList)
                genreNames.Add(genre.Value);
            movie.Genres = genreRepository.GetRangeByName(genreNames, movie);
        }

        private void MapMovieCountries(List<KeyValueItem> countries, Movie movie)
        {
            var countryNames = new List<string>(countries.Count);
            foreach (var country in countries)
                countryNames.Add(country.Value);
            movie.Countries = countryRepository.GetRangeByName(countryNames, movie);
        }

        private Movie MapTitleDataToMovie(TitleData data)
        {
            var movie = new Movie();
            movie.Title = data.Title;
            movie.Poster = data.Image;
            movie.Year = data.Year;
            movie.Cast = data.ActorList.ToString();
            movie.Plot = data.Plot;
            movie.Budget = data.BoxOffice.Budget;
            movie.BoxOffice = data.BoxOffice.CumulativeWorldwideGross;
            movie.RatingIMDb = data.IMDbRating;
            movie.TypeId = movieTypeRepository.GetByName(data.Type);
            MapMovieGenres(data.GenreList, movie);
            MapMovieCountries(data.CountryList, movie);

            return movie;
        }

        public Movie GetMovieByImdbId(string movieId)
        {
            if (string.IsNullOrEmpty(movieId))
                return null;

            var json = webClient.GetJson(string.Format(ImdbApi.movieRequest, ImdbApi.apiKey, movieId));
            webClient.Dispose();
            
            return MapTitleDataToMovie(JsonConvert.DeserializeObject<TitleData>(json));
        }

        public Top250Data GetTop250()
        {
            var json = webClient.GetJson(string.Format(ImdbApi.top250Request, ImdbApi.apiKey));
            return JsonConvert.DeserializeObject<Top250Data>(json);
        }

        public void Dispose()
        {
            webClient.Dispose();
        }
    }
}
