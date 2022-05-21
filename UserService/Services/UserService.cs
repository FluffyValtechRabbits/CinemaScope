using UserService.Interfaces;
using UserService.Dtos;
using UserService.Managers;
using UserService.Models;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System;
using Identity.Dtos;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private ApplicationUserManager _userManager => 
                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        private const string adminRole = "Administrator";

        public string UserId
        {
            get
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }

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

            foreach (var user in users.Except(admins))
            {
                var managableUser = new ManagableUserDto();
                managableUser.Name = user.UserName;
                managableUser.IsBanned = user.IsBanned;
                yield return managableUser;
            }
        }

        public void ManageBanUserByUserName(string userName)
        {
            const string adminRole = "Administrator";

            var user = _userManager.FindByName(userName);
            var result = _userManager.IsInRoleAsync(user.Id, adminRole).Result;

            if(!result)
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
