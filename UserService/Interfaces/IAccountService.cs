using Microsoft.AspNet.Identity;
using UserService.Dtos;

namespace UserService.Interfaces
{
    public interface IAccountService
    {
        bool IsAdministrator { get; }

        IdentityResult Register(RegisterDto registerDto);

        void Login(LoginDto loginDto);

        void Logout();
    }
}
