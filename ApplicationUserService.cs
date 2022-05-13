using System;
using System.Collections.Generic;
using UserService.Interfaces;
using UserService.Models;
using UserService.Dtos;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace UserService.Services
{
    public class ApplicationUserService : IService<ApplicationUserDto>
    {
        private IUnitOfWork _unitOfWork;

        public ApplicationUserService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void AddUser(ApplicationUserDto userDto)
        {
            const string userRole = "User";
            var applicationUser = Mapper.Map<ApplicationUser>(userDto);

            _unitOfWork.UserRepository.Create(applicationUser, userDto.Password);
            _unitOfWork.UserRepository.Save();

            var newUserId = _unitOfWork.UserRepository.GetByEmail(applicationUser.Email).Id;
            _unitOfWork.UserManager.AddToRole(newUserId, userRole);
            _unitOfWork.UserRepository.Save();
        }

        public void DeleteUser(ApplicationUserDto userDto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUserDto> GetAll()
        {
            var result = _unitOfWork.UserRepository.GetAll();
            return Mapper.Map<IEnumerable<ApplicationUserDto>>(result);
        }

        public ApplicationUserDto GetByEmail(string email)
        {
            var result = _unitOfWork.UserRepository.GetByEmail(email);
            return Mapper.Map<ApplicationUserDto>(result);
        }

        public ApplicationUserDto GetById(string id)
        {
            var result = _unitOfWork.UserRepository.GetById(id);
            return Mapper.Map<ApplicationUserDto>(result);
        }
    }
}
