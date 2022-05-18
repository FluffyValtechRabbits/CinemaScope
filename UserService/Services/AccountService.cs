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
using System.Collections.Generic;

namespace UserService.Services
{
    public class AccountService : IAccountService
    {
        private ApplicationUserManager _userManager => 
                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private IAuthenticationManager _authManager => 
                HttpContext.Current.GetOwinContext().Authentication;

        private const string userRole = "User";
        private const string adminRole = "Administrator";

        private string _userId = String.Empty;

        public bool IsAdministrator
        {
            get
            {
                if(_userId == String.Empty) return false;
                return _userManager.IsInRoleAsync(_userId, adminRole).Result;
            }
        }

        public IdentityResult Register(RegisterDto registerDto) 
        {
            var user = Mapper.Map<ApplicationUser>(registerDto);
            IdentityResult result = _userManager.Create(user, registerDto.Password);
            if (result.Succeeded)
                _userManager.AddToRole(user.Id, userRole);
            return result;
        }

        public void Login(LoginDto loginDto)
        {
            var user = _userManager.Find(loginDto.UserName, loginDto.Password);
            _userId = user.Id;

            var claim = _userManager.CreateIdentity(user,
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
            _userId = String.Empty;
        }

        public IdentityResult Validate(LoginDto loginDto)
        {
            var user = _userManager.FindByName(loginDto.UserName);
            var errors = new List<string>();

            if (user == null) errors.Add("A user with such user name doesn't exist.");
            else
            {
                if (user.IsBanned) errors.Add("Your account was blocked.");
                if (!_userManager.CheckPassword(user, loginDto.Password)) errors.Add("The password is wrong.");
            }

            if (errors.Count > 0)
                return IdentityResult.Failed(errors.ToArray());

            return IdentityResult.Success;
        }

    }
}
