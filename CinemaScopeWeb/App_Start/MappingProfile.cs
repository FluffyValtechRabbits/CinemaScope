﻿using AutoMapper;
using UserService.Models;
using CinemaScopeWeb.ViewModels;
using UserService.Dtos;
using System.Collections.Generic;

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
        }
    }
}