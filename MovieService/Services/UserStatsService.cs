﻿using MovieService.Dtos;
using MovieService.Interfaces;
using MovieService.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MovieService.Services
{
    public class UserStatsService : IUserStatsService
    {
        private UserToMovieRepository _userMovieRepository;
        private MovieRepository _movieRepository;

        public UserStatsService(UserToMovieRepository userMovieRepo, MovieRepository movieRepo) 
        {
            _userMovieRepository = userMovieRepo; 
            _movieRepository = movieRepo;
        }

        public IEnumerable<UserStatsMovieDto> GetDislikedMovies(string userId)
        {
            foreach (var movieId in _userMovieRepository.GetAllById(userId).Where(m => m.IsDisLiked == true).Select(m => m.MovieId))
            {
                var watchedMovie = new UserStatsMovieDto();
                var model = _movieRepository.GetById(movieId);
                watchedMovie.Title = model.Title;
                watchedMovie.Poster = model.Poster;
                watchedMovie.Id = movieId;
                yield return watchedMovie;
            }
        }

        public IEnumerable<UserStatsMovieDto> GetLikedMovies(string userId)
        {
            var jopa = _userMovieRepository.GetAllById(userId).Where(m => m.IsLiked == true).Select(m => m.MovieId);
            foreach (var movieId in jopa)
            {

                var watchedMovie = new UserStatsMovieDto();
                var model = _movieRepository.GetById(movieId);
                watchedMovie.Title = model.Title;
                watchedMovie.Poster = model.Poster;
                watchedMovie.Id = movieId;
                yield return watchedMovie;
            }
        }

        public IEnumerable<UserStatsMovieDto> GetWatchedMovies(string userId)
        {
            var jopa = _userMovieRepository.GetAllById(userId).Where(m => m.IsWatched == true).Select(m => m.MovieId);
            foreach (var movieId in jopa)
            {
                var watchedMovie = new UserStatsMovieDto();
                var model = _movieRepository.GetById(movieId);
                watchedMovie.Title = model.Title;
                watchedMovie.Poster = model.Poster;
                watchedMovie.Id = movieId;
                 yield return watchedMovie;
            } 
        }
    }
}
