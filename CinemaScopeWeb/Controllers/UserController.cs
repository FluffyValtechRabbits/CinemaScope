﻿﻿using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using AutoMapper;
using UserService.Interfaces;

namespace CinemaScopeWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]        
        public ActionResult Index()
        {
            var model = Mapper.Map<UserProfileViewModel>(_userService.GetProfile());            
            return View(model);
        }   
    }
}