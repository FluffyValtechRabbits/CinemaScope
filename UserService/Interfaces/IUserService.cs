using UserService.Dtos;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        bool IsAdministrator { get; }

        string UserId { get; }

        UserProfileDto GetProfile();
    }
}
