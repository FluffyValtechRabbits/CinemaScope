using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserService.Managers;
using CinemaScopeWeb.Models;
using Microsoft.Owin.Security;

namespace CinemaScopeWeb.Controllers
{
    public class AccountController : Controller
    {        
        private ApplicationUserManager _userManager;
        private IAuthenticationManager _authenticationManager;

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                //to do something
            }

            return View();
            //return RedirectToAction();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUserModel model)
        {
            if (!ModelState.IsValid)
            {
                //to do something
            }

            return View();
            //return RedirectToAction();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        //public ActionResult Delete()
        //{
        //    return View();
        //}

        //public ActionResult Edit()
        //{
        //    return View();
        //}

    }
}