using UserService.Interfaces;
using UserService.Dtos;
using UserService.Managers;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using AutoMapper;

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

        public bool IsAdministrator
        {
            get 
            {
                return _userManager.IsInRoleAsync(UserId, adminRole).Result;
            }
        } 

        public UserProfileDto GetProfile()
        {
            var user = _userManager.FindById(UserId);
            var userDto = Mapper.Map<UserProfileDto>(user);
            userDto.IsAdministrator = IsAdministrator;
            return userDto;
        }

    }
}
