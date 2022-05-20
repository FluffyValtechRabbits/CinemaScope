using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using AutoMapper;
using UserService.Interfaces;
using System.Linq;

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
            var model = Mapper.Map<UserProfileViewModel>(_userService.GetProfile());
            return View(model);
        }

        public ActionResult ManageUsers()
        {
            return View(_userService.GetManagableUsers().ToList());
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