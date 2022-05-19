using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using MovieService.Entities;
using MovieService.Interfaces;
using MovieService.Interfaces.ServicesInterfaces;
using Unity.Injection;

namespace CinemaScopeWeb.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IFilteringService _filteringService;

        public HomeController(IUnitOfWork unitOfWork, IFilteringService filteringService)
        {
            _unitOfWork = unitOfWork;
            _filteringService = filteringService;
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
    }
}