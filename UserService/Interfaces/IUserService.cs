using Identity.Dtos;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using UserService.Dtos;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        string UserId { get; }

        UserProfileDto GetProfile();

        IEnumerable<UserProfileDto> GetAll();

        void ManageBanUserByUserName(string userName);

        IEnumerable<ManagableUserDto> GetManagableUsers();

        IdentityResult Update(EditProfileDto userDto);

        IdentityResult ChangePassword(string oldPassword, string newPassword);
    }
}
