using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using MovieService.Entities;
using MovieService.Imdb;
using MovieService.Interfaces;
using MovieService.Interfaces.ServiceInterfaces;
using MovieService.Interfaces.ServicesInterfaces;

namespace CinemaScopeWeb.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IFilteringService _filteringService;
        private IImdbService _imdbService;

        public HomeController(IUnitOfWork unitOfWork, IFilteringService filteringService, IImdbService imdbService)
        {
            _unitOfWork = unitOfWork;
            _filteringService = filteringService;
            _imdbService = imdbService;
        }

        public ActionResult Index()
        {
            var moviesToView = _unitOfWork.MovieRepository.GetAll()
                .Select(movie => new MovieToHomeViewModel()
                {
                    Id = movie.Id,
                    Poster = movie.Poster,
                    Title = movie.Title
                }).ToList();
            var model = new HomeViewModel()
            {
                Movies = moviesToView,
                Genres = _unitOfWork.GenreRepository.GetAll().Select(x => x.Name).Distinct().OrderByDescending(x => x).ToList(),
                Countries = _unitOfWork.CountryRepository.GetAll().Select(x => x.Name).Distinct().OrderByDescending(x => x).ToList(),
                Types = _unitOfWork.MovieTypeRepository.GetAll().Select(x => x.Name).Distinct().OrderByDescending(x => x).ToList(),
                Years = _unitOfWork.MovieRepository.GetAll().Select(x => int.Parse(x.Year)).Distinct().OrderByDescending(x => x).ToList(),
                IsWatched = false
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchResult(string input)
        {
            if (input.IsNullOrWhiteSpace())
                return RedirectToAction("Index");
            var moviesToView = _unitOfWork.MovieRepository.GetAll()
                .Select(movie => new MovieToHomeViewModel()
                {
                    Id = movie.Id,
                    Poster = movie.Poster,
                    Title = movie.Title
                }).ToList();
            var inputRegex = new Regex($"(\\b{input.ToUpper()})|(\\b{input.ToUpper()}\\b)");
            var movieWithFiltering = moviesToView
                .Where(word => inputRegex.IsMatch(word.Title.ToUpper()))
                .ToList();
            var model = new HomeViewModel()
            {
                Movies = movieWithFiltering,
                Genres = _unitOfWork.GenreRepository.GetAll().Select(x=>x.Name).Distinct().OrderByDescending(x => x).ToList(),
                Countries = _unitOfWork.CountryRepository.GetAll().Select(x=>x.Name).Distinct().OrderByDescending(x => x).ToList(),
                Types = _unitOfWork.MovieTypeRepository.GetAll().Select(x=>x.Name).Distinct().OrderByDescending(x => x).ToList(),
                Years = _unitOfWork.MovieRepository.GetAll().Select(x=>int.Parse(x.Year)).Distinct().OrderByDescending(x=>x).ToList(),
                IsWatched = false
            };

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult FilteringResult(List<string> genres, 
            List<string> countries, List<string> types, List<string> years, bool isWatched=false)
        {
            var movies = _unitOfWork.MovieRepository.GetAll().ToList();
            _filteringService.FilterByCountries(countries,movies);
            _filteringService.FilterByGenres(genres,movies);
            _filteringService.FilterByYears(years,movies);
            _filteringService.FilterByType(types,movies);
            if (User.Identity.IsAuthenticated)
                _filteringService.FilterByWatched(isWatched, movies, User.Identity.GetUserId());
            
            var moviesWithFiltering = movies
                .Select(movie => new MovieToHomeViewModel()
                {
                    Id = movie.Id,
                    Poster = movie.Poster,
                    Title = movie.Title
                }).ToList();

            var model = new HomeViewModel()
            {
                Movies = moviesWithFiltering,
                Genres = _unitOfWork.GenreRepository.GetAll().Select(x => x.Name).Distinct().OrderByDescending(x => x).ToList(),
                Countries = _unitOfWork.CountryRepository.GetAll().Select(x => x.Name).Distinct().OrderByDescending(x => x).ToList(),
                Types = _unitOfWork.MovieTypeRepository.GetAll().Select(x => x.Name).Distinct().OrderByDescending(x => x).ToList(),
                Years = _unitOfWork.MovieRepository.GetAll().Select(x => int.Parse(x.Year)).Distinct().OrderByDescending(x => x).ToList(),
                IsWatched = false
            };
            return View("Index", model);
        }

        public ActionResult Update()
        {
            for(int i = 0; i < 20; i++)
            {
                string newMovieId;
                var lastLoadedMovie = _unitOfWork.MovieRepository.GetLastUploaded();
                if (lastLoadedMovie != null)
                {
                    newMovieId  = AddId(lastLoadedMovie.ImdbId);
                }
                else
                {
                    newMovieId = ImdbApi.MoiveIdCode + ImdbApi.MovieIdStartNumber;
                }
                var result = AddNewMovie(newMovieId);
                //var counter = 0;
                if (!result)
                {
                    newMovieId = AddId(newMovieId);
                    result =  AddNewMovie(newMovieId);
                    //counter++;                    
                }                
            }            

            return RedirectToAction("Index");
        }

        private string AddId(string id)
        {
            var lastLoadedId = id;
            var lastLoadedIdNumber = int.Parse(lastLoadedId.Replace(ImdbApi.MoiveIdCode, ""));
            lastLoadedIdNumber += 1;
            //- lastLoadedIdNumber.ToString().Length
            var newMovieNumber = lastLoadedIdNumber.ToString().PadLeft(ImdbApi.MovieIdStartNumber.Length, '0');
            return ImdbApi.MoiveIdCode + newMovieNumber;
        }

        private bool AddNewMovie(string newMovieId)
        {
            return _imdbService.GetMovieByImdbId(newMovieId);
        }
    }
}