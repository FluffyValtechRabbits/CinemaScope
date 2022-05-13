using System.Web.Mvc;
using CinemaScopeWeb.Models;
using Microsoft.Owin.Security;
using AutoMapper;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using UserService.Managers;
using UserService.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.Owin.Security.Cookies;
using System;

namespace CinemaScopeWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private ApplicationRoleManager _roleManager => HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        private IAuthenticationManager _authManager => HttpContext.GetOwinContext().Authentication;  
        
        private const string userRole = "User";

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<ApplicationUser>(model);
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user.Id, userRole);    
                    var loginModel = Mapper.Map<LoginUserViewModel>(model);
                    return RedirectToAction("Login", loginModel);
                }
                else
                {
                    return Content("Error");
                }
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
        public async Task<ActionResult> Login(LoginUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //to do something
            }

            var userCreated = await _userManager.FindAsync(model.UserName, model.Password);

            var claim = await _userManager.CreateIdentityAsync(userCreated,
                        DefaultAuthenticationTypes.ApplicationCookie);
            _authManager.SignOut();
            _authManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(30)
            }, claim);


            var profileModel = Mapper.Map<UserProfileViewModel>(userCreated);
            return RedirectToAction("Profile", "User", profileModel);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _authManager.SignOut();
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