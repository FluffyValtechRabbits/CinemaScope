using UserService.Dtos;

namespace UserService.Interfaces
{
    public interface IAccountService
    {
        bool Register(RegisterDto registerDto);

        void Login(LoginDto loginDto);

        void Logout();
    }
}
