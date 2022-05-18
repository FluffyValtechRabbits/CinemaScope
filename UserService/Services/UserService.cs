using UserService.Interfaces;
using UserService.Dtos;
using UserService.Managers;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private ApplicationUserManager _userManager => 
                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

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

    }
}
