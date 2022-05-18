using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using Microsoft.Ajax.Utilities;
using MovieService.Entities;
using MovieService.Interfaces;
using Unity.Injection;

namespace CinemaScopeWeb.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return View(moviesToView);
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
            return View("Index", movieWithFiltering);
        }
    }
}