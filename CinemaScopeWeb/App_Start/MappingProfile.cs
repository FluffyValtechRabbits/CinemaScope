﻿using AutoMapper;
using CinemaScopeWeb.ViewModels;
using Identity.Dtos;
using Identity.Models;

namespace CinemaScopeWeb.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, RegisterDto>().ReverseMap();
            Mapper.CreateMap<ApplicationUser, LoginDto>().ReverseMap();
            Mapper.CreateMap<ApplicationUser, UserProfileDto>().ReverseMap();
            Mapper.CreateMap<ApplicationUser, ManagableUserDto>().ReverseMap();
            Mapper.CreateMap<ApplicationUser, EditProfileDto>().ReverseMap();

            Mapper.CreateMap<RegisterDto, LoginDto>().ReverseMap();
            Mapper.CreateMap<RegisterDto, RegisterUserViewModel>().ReverseMap();

            Mapper.CreateMap<LoginDto, LoginUserViewModel>().ReverseMap();

            Mapper.CreateMap<UserProfileDto, UserProfileViewModel>().ReverseMap();
            Mapper.CreateMap<UserProfileDto, EditUserProfileViewModel>().ReverseMap();

            Mapper.CreateMap<EditProfileDto, EditUserProfileViewModel>().ReverseMap();

            Mapper.CreateMap<AboutUsDto, AboutUser>().ReverseMap();
            Mapper.CreateMap<AboutUsDto, AboutUsViewModel>().ReverseMap();

            Mapper.CreateMap<CreateAboutUsDto, AboutUser>().ReverseMap();
            Mapper.CreateMap<CreateAboutUsDto, CreateAboutUsViewModel>().ReverseMap();    
            
            Mapper.CreateMap<ManagableUserDto, ManagableUserViewModel>().ReverseMap();
        }
    }
}