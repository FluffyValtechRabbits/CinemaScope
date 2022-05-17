using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using MovieService.Entities;
using MovieService.Interfaces;

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
                .ToList()
                .Select(movie => new MovieToHomeViewModel()
                {
                    Id = movie.Id,
                    Poster = movie.Poster,
                    Title = movie.Title
                }).ToList();
            return View(moviesToView);
        }
    }
}