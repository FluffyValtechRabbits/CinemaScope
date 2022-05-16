using MovieService.Repositories;
using System.Web.Mvc;
using MovieService.Interfaces;

namespace CinemaScopeWeb.Controllers
{
    public class MoviesController : Controller
    {
        MovieRepository _movieRepository;
        IImdbService _imdbService;
        IRatingService _ratingService;

        public MoviesController(MovieRepository movieRepo, IImdbService imdbService, IRatingService ratingService) 
        { 
            _movieRepository = movieRepo;
            _imdbService = imdbService;
            _ratingService = ratingService;
        }

        public ActionResult Get(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null)
                return View("NoMovie");
            return View(movie);
        }

        public ActionResult Top250()
        {
            var data = _imdbService.GetTop250();
            if (!string.IsNullOrEmpty(data.ErrorMessage))
                return View("Error");
            return View(data.Items);
        }

        public ActionResult MostWatched()
        {
            return View(_ratingService.MostWatched());
        }

        public ActionResult MostLiked()
        {
            return View(_ratingService.MostLiked());
        }
    }
}