using AutoMapper;
using UserService.Models;
using CinemaScopeWeb.Models;

namespace CinemaScopeWeb.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, RegisterUserViewModel>().ReverseMap();
            Mapper.CreateMap<ApplicationUser, UserProfileViewModel>().ReverseMap();
            Mapper.CreateMap<RegisterUserViewModel, LoginUserViewModel>().ReverseMap();
        }
    }
}