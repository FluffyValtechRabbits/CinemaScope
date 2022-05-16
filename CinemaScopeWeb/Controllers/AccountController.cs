﻿using System.Web.Mvc;
using AutoMapper;
using UserService.Dtos;
using UserService.Interfaces;
using CinemaScopeWeb.Models;

namespace CinemaScopeWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAccountService _userService;

        public AccountController(IAccountService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDto = Mapper.Map<RegisterDto>(model);
                var result = _userService.Register(userDto);
                if(!result) return Content("Error");
                _userService.Login(Mapper.Map<LoginDto>(userDto));
                return RedirectToAction("Index", "User");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.Login(Mapper.Map<LoginDto>(model));
                return RedirectToAction("Index", "User");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _userService.Logout();
            return RedirectToAction("Login");
        }
    }
}