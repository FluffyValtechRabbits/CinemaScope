using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieService.Entities;
using MovieService.Interfaces;
using MovieService.Interfaces.ServicesInterfaces;
using Sentry;

namespace MovieService.Services
{
    public class FilteringService : IFilteringService
    {
        private  IUnitOfWork _unitOfWork;
        public FilteringService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void FilterByCountries(List<string> countries, List<Movie> movies)
        {
            var size = movies.Count;
            if (countries != null)
            {
                for (var index = 0; index < size; index++)
                {
                    var movie = movies[index];
                    var filteredByCountry = movie.Countries.Select(x => x.Name).Intersect(countries);
                    if (filteredByCountry.Count() != countries.Count)
                    {
                        movies.Remove(movie);
                        size--;
                        index--;
                    }
                }
            }
        }
        
        public void FilterByGenres(List<string> genres, List<Movie> movies)
        {
            var size = movies.Count;
            if (genres != null)
            {
                for (var index = 0; index < size; index++)
                {
                    var movie = movies[index];
                    var filteredByGenre = movie.Genres.Select(x => x.Name).Intersect(genres);
                    if (filteredByGenre.Count() != genres.Count)
                    {
                        movies.Remove(movie);
                        size--;
                        index--;
                    }

                }
            }
        }
        
        public void FilterByType(List<string> types, List<Movie> movies)
        {
            var size = movies.Count;
            if (types != null)
            {
                for (var index = 0; index < size; index++)
                {
                    var movie = movies[index];
                    if (!types.Contains(movie.Type.Name))
                    {
                        movies.Remove(movie);
                        size--;
                        index--;
                    }
                }
            }
        }
        public void FilterByYears(List<string> years, List<Movie> movies)
        {
            var size = movies.Count;
            if (years != null)
            {
                for (var index = 0; index < size; index++)
                {
                    var movie = movies[index];
                    if (!years.Contains(movie.Year))
                    {
                        movies.Remove(movie);
                        size--;
                        index--;
                    }
                }
            }
        }
        
        public void FilterByWatched(bool isWatched, List<Movie> movies, string userId)
        {
            if (!isWatched) return;
            var size = movies.Count;
            var userToMovie = _unitOfWork.UserToMovieRepository.GetAll().ToList();
            for (var index = 0; index < size; index++)
            {
                var movie = movies[index];
                var userToMovieWatched = userToMovie.FirstOrDefault(x =>
                    x.MovieId == movie.Id && x.ApplicationUserId == userId && x.IsWatched);
                if (userToMovieWatched==null)
                {
                    movies.Remove(movie);
                    size--;
                    index--;
                }
            }
        }
    }
}
