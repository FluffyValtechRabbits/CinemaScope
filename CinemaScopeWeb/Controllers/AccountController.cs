using System.Web.Mvc;
using CinemaScopeWeb.ViewModels;
using AutoMapper;
using UserService.Dtos;
using UserService.Interfaces;

namespace CinemaScopeWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService userService)
        {
            _accountService = userService;
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

            if (!ModelState.IsValid) return View(model);

            var userDto = Mapper.Map<RegisterDto>(model);
            var result = _accountService.Register(userDto);

            if (!result.Succeeded) return View(model);

            _accountService.Login(Mapper.Map<LoginDto>(userDto));
            return RedirectToAction("Index", "User");
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
            if (!ModelState.IsValid) return View(model);

            _accountService.Login(Mapper.Map<LoginDto>(model));

            if (_accountService.IsAdministrator)
                return RedirectToAction("Index", "Admin");
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            _accountService.Logout();
            return RedirectToAction("Login");
        }
    }
}