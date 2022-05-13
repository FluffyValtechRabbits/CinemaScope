﻿using MovieService.Repositories;
using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using Imdb;
using Microsoft.AspNet.Identity;
using MovieService.Entities;

namespace CinemaScopeWeb.Controllers
{
    public class MoviesController : Controller
    {
        private MovieRepository _movieRepository;
        private UserToMovieRepository _userToMovieRepository;
        private ImdbService _imdbService;

        public MoviesController(MovieRepository movieRepo, UserToMovieRepository userToMovieRepository, ImdbService imdbService)
        {
            _movieRepository = movieRepo;
            _imdbService = imdbService;
            _userToMovieRepository = userToMovieRepository;
        }

        public ActionResult Get(int id)
        {
            var movie = new MovieViewModel() {Movie = _movieRepository.GetById(id)};
            var userId = User.Identity.GetUserId();
            var userToMovie = _userToMovieRepository.GetOneByUserAndMovieIds(userId, id);
            if (User.Identity.IsAuthenticated && !(userToMovie is null))
            {
                movie.IsLiked = userToMovie.IsLiked;
                movie.IsWatched = userToMovie.IsWatched;
                movie.IsDisliked = userToMovie.IsDisLiked;
            }
            return movie == null ? View("NoMovie") : View(movie);
        }

        public ActionResult LikeMovie(int id)
        {
            var movie = _movieRepository.GetById(id);
            var userId = User.Identity.GetUserId();
            var userToMovie = _userToMovieRepository.GetOneByUserAndMovieIds(userId, id);
            if (userToMovie is null)
            {
                userToMovie = new UserToMovie()
                {
                    IsDisLiked = false,
                    ApplicationUserId = userId,
                    IsLiked = true,
                    IsWatched = false,
                    MovieId = id
                };
                _userToMovieRepository.Add(userToMovie);
            }
            else if (userToMovie.IsLiked)
            {
                userToMovie.IsLiked = false;
                _userToMovieRepository.Delete(userToMovie);
                _userToMovieRepository.Save();
            }
            else
            {
                if (userToMovie.IsDisLiked)
                    userToMovie.IsDisLiked = false;
                userToMovie.IsLiked = true;
                _userToMovieRepository.Update(userToMovie);
                _userToMovieRepository.Save();
            }
            var movieViewModel = new MovieViewModel()
            {
                Movie = movie,
                IsDisliked = userToMovie.IsDisLiked,
                IsLiked = userToMovie.IsLiked,
                IsWatched = userToMovie.IsWatched
            };
            return View("Get", movieViewModel);
        }
        
        public ActionResult DislikeMovie(int id)
        {
            var movie = _movieRepository.GetById(id);
            var userId = User.Identity.GetUserId();
            var userToMovie = _userToMovieRepository.GetOneByUserAndMovieIds(userId, id);
            if (userToMovie is null)
            {
                userToMovie = new UserToMovie()
                {
                    IsDisLiked = true,
                    ApplicationUserId = userId,
                    IsLiked = false,
                    IsWatched = false,
                    MovieId = id
                };
                _userToMovieRepository.Add(userToMovie);
            }
            else if (userToMovie.IsDisLiked)
            {
                userToMovie.IsDisLiked = false;
                _userToMovieRepository.Delete(userToMovie);
                _userToMovieRepository.Save();
            }
            else
            {
                if (userToMovie.IsLiked)
                    userToMovie.IsLiked = false;
                userToMovie.IsDisLiked = true;
                _userToMovieRepository.Update(userToMovie);
                _userToMovieRepository.Save();
            }
            var movieViewModel = new MovieViewModel()
            {
                Movie = movie,
                IsDisliked = userToMovie.IsDisLiked,
                IsLiked = userToMovie.IsLiked,
                IsWatched = userToMovie.IsWatched
            };
            return View("Get", movieViewModel);
        }
        
        public ActionResult MarkAsWatched(int id)
        {
            var movie = _movieRepository.GetById(id);
            var userId = User.Identity.GetUserId();
            var userToMovie = _userToMovieRepository.GetOneByUserAndMovieIds(userId, id);
            
            if (userToMovie is null)
            {
                userToMovie = new UserToMovie()
                {
                    IsDisLiked = false,
                    ApplicationUserId = userId,
                    IsLiked = false,
                    IsWatched = true,
                    MovieId = id
                };
                _userToMovieRepository.Add(userToMovie);
            }
            else if (userToMovie.IsWatched)
            {
                userToMovie.IsWatched = false;
                _userToMovieRepository.Update(userToMovie);
                _userToMovieRepository.Save();
            }
            else
            {
                userToMovie.IsWatched = true;
                _userToMovieRepository.Update(userToMovie);
                _userToMovieRepository.Save();
            }
            var movieViewModel = new MovieViewModel() 
            { 
                    Movie = movie, 
                    IsDisliked = userToMovie.IsDisLiked, 
                    IsLiked = userToMovie.IsLiked, 
                    IsWatched = userToMovie.IsWatched
            };

            return View("Get", movieViewModel);
        }

        public ActionResult Top250()
        {
            var data = _imdbService.GetTop250();
            if (!string.IsNullOrEmpty(data.ErrorMessage))
                return View("Error");
            return View(data.Items);
        }
    }
}