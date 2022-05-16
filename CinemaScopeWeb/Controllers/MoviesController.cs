using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using Microsoft.AspNet.Identity;
using MovieService.Interfaces;
using MovieService.Interfaces.ServicesInterfaces;

namespace CinemaScopeWeb.Controllers
{
    public class MoviesController : Controller
    {
        private IUnitOfWork _unitOfWork;
        IImdbService _imdbService;
        IMoviesService _moviesService;

        public MoviesController(IUnitOfWork unitOfWork, IImdbService imdbService, IMoviesService moviesService)
        {
            _unitOfWork = unitOfWork;
            _imdbService = imdbService;
            _moviesService = moviesService;
        }

        public ActionResult Get(int id)
        {
            var movie = new MovieViewModel()
            {
                Movie = _unitOfWork.MovieRepository.GetById(id)
            };
            var userId = User.Identity.GetUserId();
            var userToMovie = _unitOfWork.UserToMovieRepository.GetOneByUserAndMovieIds(userId, id);
            if (User.Identity.IsAuthenticated && !(userToMovie is null))
            {
                movie.IsLiked = userToMovie.IsLiked;
                movie.IsWatched = userToMovie.IsWatched;
                movie.IsDisliked = userToMovie.IsDisLiked;
            }
            return movie == null ? View("NoMovie") : View(movie);
        }

        [Authorize]
        public ActionResult LikeMovie(int id)
        {
            var userId = User.Identity.GetUserId();
            _moviesService.LikeMovie(userId, id);
            return RedirectToAction("Get", new { id });
        }

        [Authorize]
        public ActionResult DislikeMovie(int id)
        {
            var userId = User.Identity.GetUserId();
            _moviesService.DislikeMovie(userId, id);
            return RedirectToAction("Get", new {id});
        }

        [Authorize]
        public ActionResult MarkAsWatched(int id)
        {
            var userId = User.Identity.GetUserId();
            _moviesService.MarkAsWatched(userId, id);
            return RedirectToAction("Get", new { id });
        }

        public ActionResult Top250()
        {
            var data = _imdbService.GetTop250();
            return !string.IsNullOrEmpty(data.ErrorMessage) ? View("Error") : View(data.Items);
        }

        public ActionResult MostWatched()
        {
            return View(_moviesService.MostWatched());
        }

        public ActionResult MostLiked()
        {
            return View(_moviesService.MostLiked());
        }
    }
}