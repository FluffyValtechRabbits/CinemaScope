﻿using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using AutoMapper;
using Identity.Interfaces;
using MovieService.Interfaces;
using System.Linq;
using Identity.Dtos;

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

        [HttpGet]
        public ActionResult Edit()
        {
            var model = Mapper.Map<EditUserProfileViewModel>(_userService.GetProfile());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserProfileViewModel model)
        {
            if(!ModelState.IsValid) return View(model);

            var result = _userService.Update(Mapper.Map<EditProfileDto>(model));
            if (result.Succeeded && 
                model.OldPassword != null && 
                model.Password != null)
                result = _userService.ChangePassword(model.OldPassword, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error);
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}