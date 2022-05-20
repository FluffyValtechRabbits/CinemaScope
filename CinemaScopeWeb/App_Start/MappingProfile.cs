using AutoMapper;
using UserService.Models;
using CinemaScopeWeb.ViewModels;
using UserService.Dtos;
using System.Collections.Generic;
using MovieService.Entities;
using MovieService.Dtos;

namespace CinemaScopeWeb.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<RegisterDto, RegisterUserViewModel>().ReverseMap();
            Mapper.CreateMap<UserProfileDto, UserProfileViewModel>().ReverseMap();
            Mapper.CreateMap<LoginDto, LoginUserViewModel>().ReverseMap();
            Mapper.CreateMap<RegisterDto, ApplicationUser>().ReverseMap();
            Mapper.CreateMap<LoginDto, ApplicationUser>().ReverseMap();
            Mapper.CreateMap<RegisterDto, LoginDto>().ReverseMap();
            Mapper.CreateMap<ApplicationUser, UserProfileDto>().ReverseMap();
            Mapper.CreateMap<IEnumerable<UserProfileDto>, IEnumerable<ApplicationUser>>();
            Mapper.CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.TypeId, opt => opt.Ignore())
                .ForMember(dest => dest.MovieTypes, opt => opt.Ignore())
                .ForMember(dest => dest.GenreIds, opt => opt.Ignore())
                .ForMember(dest => dest.CountryIds, opt => opt.Ignore())
                .ForMember(dest => dest.GenreList, opt => opt.Ignore())
                .ForMember(dest => dest.CountriesList, opt => opt.Ignore());
        }
    }
}