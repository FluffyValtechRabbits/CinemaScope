using UserService.Dtos;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        string UserId { get; }

        UserProfileDto GetProfile();
    }
}
