using MovieService.Repositories;
using System.Web.Mvc;

namespace CinemaScopeWeb.Controllers
{
    public class MoviesController : Controller
    {
        MovieRepository _movieRepository;

        public MoviesController(MovieRepository movieRepo) { _movieRepository = movieRepo; }

        public ActionResult Get(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null)
                return View("NoMovie");
            return View("Movie", movie);
        }
    }
}