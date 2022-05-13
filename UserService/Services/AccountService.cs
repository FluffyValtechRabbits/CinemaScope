using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System;
using System.Web;
using AutoMapper;
using UserService.Managers;
using UserService.Dtos;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Services
{
    public class AccountService : IAccountService
    {
        private ApplicationUserManager _userManager => 
                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private IAuthenticationManager _authManager => 
                HttpContext.Current.GetOwinContext().Authentication;

        private const string userRole = "User";

        public bool Register(RegisterDto registerDto) 
        {
            var user = Mapper.Map<ApplicationUser>(registerDto);
            IdentityResult result = _userManager.Create(user, registerDto.Password);
            if (result.Succeeded)
            {
                _userManager.AddToRole(user.Id, userRole);
                return true;
            }

            return false;
        }

        public void Login(LoginDto loginDto)
        {
            var userCreated = _userManager.Find(loginDto.UserName, loginDto.Password);

            var claim = _userManager.CreateIdentity(userCreated,
                        DefaultAuthenticationTypes.ApplicationCookie);

            _authManager.SignOut();
            _authManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(30)
            }, claim);
        }

        public void Logout()
        {
            _authManager.SignOut();
        }

    }
}
