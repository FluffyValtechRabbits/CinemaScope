﻿using System.Threading.Tasks;
﻿using System.Web.Mvc;
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