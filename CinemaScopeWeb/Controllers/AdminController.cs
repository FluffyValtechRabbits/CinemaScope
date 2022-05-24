using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using CinemaScopeWeb.ViewModels;
using Identity.Interfaces;

namespace CinemaScopeWeb.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
        
        public ActionResult Index()
        {
            var profile = _userService.GetProfile();
            var model = Mapper.Map<UserProfileViewModel>(profile);
            return View(model);
        }

        public ActionResult ManageUsers()
        {
            var users = _userService.GetManagableUsers().ToList();
            var model = Mapper.Map<List<ManagableUserViewModel>>(users);
            return View(model);
        }

        public ActionResult ManageUserBan(string userName)
        {
            if (userName == null)
                return View("Error");

            _userService.ManageBanUserByUserName(userName);

            return RedirectToAction("ManageUsers");
        }
    }
}