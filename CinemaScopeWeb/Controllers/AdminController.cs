using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using AutoMapper;
using UserService.Interfaces;
using System.Linq;
using MovieService.Interfaces.ServiceInterfaces;
using MovieService.Dtos;

namespace CinemaScopeWeb.Controllers
{
    public class AdminController : Controller
    {
        private IUserService _userService;
        private IMovieService _movieService;

        public AdminController(IUserService userService, IMovieService movieService)
        {
            _userService = userService;
            _movieService = movieService;   
        }
        
        public ActionResult Index()
        {
            var model = Mapper.Map<UserProfileViewModel>(_userService.GetProfile());
            return View(model);
        }

        public ActionResult ManageUsers()
        {
            return View(_userService.GetManagableUsers().ToList());
        }

        public ActionResult ManageUserBan(string userName)
        {
            if (userName == null)
                return View("Error");

            _userService.ManageBanUserByUserName(userName);

            return RedirectToAction("ManageUsers");
        }

        public ActionResult ManageMovies()
        {
            return View(_movieService.GetManagedMovies().ToList());
        }

        public ActionResult DeleteMovie(int id)
        {
            _movieService.DeleteMovie(id);
            return RedirectToAction("ManageMovies");
        }

        public ActionResult ManageMovie(int id=0)
        {
            if (id == 0)
            {
                var movieDto = new MovieDto();
                movieDto.CountriesList = _movieService.PopulateCountriesList(movieDto.CountryIds);
                movieDto.GenreList = _movieService.PopulateGenresList(movieDto.GenreIds);
                movieDto.MovieTypes = _movieService.PopulateMovieTypeList(movieDto.TypeId);
                return View(movieDto);
            }

            var movie = _movieService.GetMovieById(id);
            if (movie == null)
                return View("Error");

            var dto = Mapper.Map<MovieDto>(movie);
            dto.CountriesList = _movieService.PopulateCountriesList(dto.CountryIds);
            dto.GenreList = _movieService.PopulateGenresList(dto.GenreIds);
            dto.MovieTypes = _movieService.PopulateMovieTypeList(dto.TypeId);

            foreach (var genre in movie.Genres)
                dto.GenreIds.Add(genre.Id);
             
            foreach(var country in movie.Countries)
                dto.CountryIds.Add(country.Id);

            return View(dto);
        }

        [HttpPost]
        public ActionResult CreateUpdateMovie(MovieDto movie)
        {
            if (!ModelState.IsValid)
            {
                movie.CountriesList = _movieService.PopulateCountriesList(movie.CountryIds);
                movie.GenreList = _movieService.PopulateGenresList(movie.GenreIds);
                movie.MovieTypes = _movieService.PopulateMovieTypeList(movie.TypeId);
                return View("ManageMovie", movie);
            }
                
            _movieService.CreateUpdate(movie);
            return RedirectToAction("ManageMovies");
        }
    }
}
