using MovieService.Repositories;
using System.Web.Mvc;
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
            var movie = _movieRepository.GetById(id);
            return movie == null ? View("NoMovie") : View(movie);
        }

        public ActionResult LikeMovie(int id)
        {
            var movie = _movieRepository.GetById(id);
            var userId = User.Identity.GetUserId();
            var userToMovie = new UserToMovie()
            {
                IsDisLiked = false,
                ApplicationUserId = userId,
                IsLiked = true,
                IsWatched = true,
                MovieId = id
            };
            _userToMovieRepository.Add(userToMovie);
            return View("Get", movie);
        }
        
        public ActionResult DislikeMovie(int id)
        {
            var movie = _movieRepository.GetById(id);
            var userId = User.Identity.GetUserId();
            var userToMovie = new UserToMovie()
            {
                IsDisLiked = false,
                ApplicationUserId = userId,
                IsLiked = true,
                IsWatched = true,
                MovieId = id
            };
            _userToMovieRepository.Add(userToMovie);
            return View("Get", movie);
        }
        
        public ActionResult MarkAsWatched(int id)
        {
            var movie = _movieRepository.GetById(id);
            var userId = User.Identity.GetUserId();
            var userToMovie = new UserToMovie()
            {
                IsDisLiked = false,
                ApplicationUserId = userId,
                IsLiked = true,
                IsWatched = true,
                MovieId = id
            };
            _userToMovieRepository.Add(userToMovie);
            return View("Get", movie);
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