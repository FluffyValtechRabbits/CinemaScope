﻿using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using AutoMapper;
using UserService.Interfaces;
using MovieService.Interfaces;
using System.Linq;

namespace CinemaScopeWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IUserStatsService _userStatsService;

        public UserController(IUserService userService, IUserStatsService userStats)
        {
            _userService = userService;
            _userStatsService = userStats;
        }

        [HttpGet]        
        public ActionResult Index()
        {
            var model = Mapper.Map<UserProfileViewModel>(_userService.GetProfile());
            model.WatchedMovies = _userStatsService.GetWatchedMovies(_userService.UserId).ToList();
            model.LikedMovies = _userStatsService.GetLikedMovies(_userService.UserId).ToList();
            model.DislikedMovies = _userStatsService.GetDislikedMovies(_userService.UserId).ToList();
            return View(model);
        }  
    }
}