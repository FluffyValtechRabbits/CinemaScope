using MovieService.Repositories;
using System.Web.Mvc;
using Imdb;

namespace CinemaScopeWeb.Controllers
{
    public class MoviesController : Controller
    {
        MovieRepository _movieRepository;
        ImdbService _imdbService;

        public MoviesController(MovieRepository movieRepo, ImdbService imdbService) { _movieRepository = movieRepo; _imdbService = imdbService; }

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
    }
}