using IMDbApiLib.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using MovieService.Entities;
using MovieService.Imdb;
using System;
using System.Linq;
using Imdb;
using MovieService.Interfaces;
using MovieService.Interfaces.ServicesInterfaces;

namespace Services
{
    public class ImdbService : IDisposable, IImdbService
    {
        private IUnitOfWork _unitOfWork;
        private CustomHttpClient _webClient;

        public ImdbService(IUnitOfWork unitOfWork,CustomHttpClient webClient)
        {
            _unitOfWork = unitOfWork;
            _webClient = webClient;
        }

        public List<SearchOption> SearchMovie(string searchOption)
        {
            if (string.IsNullOrEmpty(searchOption))
                return null;

            var json = _webClient.GetJson(string.Format(ImdbApi.searchRequest, ImdbApi.apiKey, searchOption));
            _webClient.Dispose();
            Console.WriteLine(json);
            var obj = JsonConvert.DeserializeObject<SearchData>(json);
            var result = new List<SearchOption>(obj.Results.Count);
            result.AddRange(obj.Results.Select(item => new SearchOption(item.Id, item.Title)));

            return result;
        }

        private void MapMovieGenres(List<KeyValueItem> genreList, Movie movie)
        {
            var genreNames = new List<string>(genreList.Count);
            genreNames.AddRange(genreList.Select(genre => genre.Value));
            movie.Genres = _unitOfWork.GenreRepository.GetRangeByName(genreNames, movie);
        }

        private void MapMovieCountries(List<KeyValueItem> countries, Movie movie)
        {
            var countryNames = new List<string>(countries.Count);
            countryNames.AddRange(countries.Select(country => country.Value));
            movie.Countries = _unitOfWork.CountryRepository.GetRangeByName(countryNames, movie);
        }

        private Movie MapTitleDataToMovie(TitleData data)
        {
            var movie = new Movie
            {
                Title = data.Title,
                Poster = data.Image,
                Year = data.Year,
                Cast = data.ActorList.ToString(),
                Plot = data.Plot,
                Budget = data.BoxOffice.Budget,
                BoxOffice = data.BoxOffice.CumulativeWorldwideGross,
                RatingIMDb = data.IMDbRating,
                TypeId = _unitOfWork.MovieTypeRepository.GetByName(data.Type)
            };
            MapMovieGenres(data.GenreList, movie);
            MapMovieCountries(data.CountryList, movie);

            return movie;
        }

        public Movie GetMovieByImdbId(string movieId)
        {
            if (string.IsNullOrEmpty(movieId))
                return null;

            var json = _webClient.GetJson(string.Format(ImdbApi.movieRequest, ImdbApi.apiKey, movieId));
            _webClient.Dispose();
            
            return MapTitleDataToMovie(JsonConvert.DeserializeObject<TitleData>(json));
        }

        public Top250Data GetTop250()
        {
            var json = _webClient.GetJson(string.Format(ImdbApi.top250Request, ImdbApi.apiKey));
            return JsonConvert.DeserializeObject<Top250Data>(json);
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}
