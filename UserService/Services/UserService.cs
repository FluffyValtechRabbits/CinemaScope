using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using AutoMapper;
using Identity.Interfaces;
using Identity.Dtos;
using Identity.Managers;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private ApplicationUserManager _userManager => 
                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        private const string adminRole = "Administrator";

        public string UserId => HttpContext.Current.User.Identity.GetUserId();

        public UserProfileDto GetProfile()
        {
            var user = _userManager.FindById(UserId);
            var userDto = Mapper.Map<UserProfileDto>(user);
            return userDto;
        }

        public IEnumerable<UserProfileDto> GetAll()
        {
            var users = _userManager.Users.ToList();
            return Mapper.Map<IEnumerable<UserProfileDto>>(users);
        }


        public IEnumerable<ManagableUserDto> GetManagableUsers()
        {
            var users = _userManager.Users.ToList();
            var admins = users.Where(u => _userManager.IsInRole(u.Id, adminRole));
            var usersDto = Mapper.Map<IEnumerable<ManagableUserDto>>(users.Except(admins));
            return usersDto;
        }

        public void ManageBanUserByUserName(string userName)
        {
            var user = _userManager.FindByName(userName);
            var isAdmin = _userManager.IsInRoleAsync(user.Id, adminRole).Result;

            if(!isAdmin)
            {
                user.IsBanned = !user.IsBanned;                
                _userManager.Update(user);
            }
        }

        public IdentityResult Update(EditProfileDto userDto)
        {
            var user = _userManager.FindById(UserId);
            if (user is null)
                throw new ArgumentNullException("User was not found.");            
            user = Mapper.Map(userDto, user);           
            var result = _userManager.Update(user);
            return result;
        }

        public IdentityResult ChangePassword(string oldPassword, string newPassword)
        {
            var errors = new List<string>();

            if (oldPassword == null && newPassword == null)
            {
                errors.Add("Passwords are required.");
                return IdentityResult.Failed(errors.ToArray());
            }

            if (oldPassword.Equals(newPassword))
            {
                errors.Add("Old and new passwords must be different.");
                return IdentityResult.Failed(errors.ToArray());
            }

            var result = _userManager.ChangePassword(UserId, oldPassword, newPassword);

            return result;
        }
    }
}
