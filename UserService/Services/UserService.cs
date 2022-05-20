using UserService.Interfaces;
using UserService.Dtos;
using UserService.Managers;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
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
    }
}
