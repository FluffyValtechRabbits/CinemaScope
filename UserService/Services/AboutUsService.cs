using AutoMapper;
using System.Collections.Generic;
using UserService.Interfaces;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Services
{
    public class AboutUsService : IAboutUsService
    {
        private IUnitOfWork _unitOfWork;

        public AboutUsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AboutUsDto> GetAll()
        {
            var users = _unitOfWork.AboutUsRepository.GetAll();
            return Mapper.Map<IEnumerable<AboutUsDto>>(users);
        }

        public AboutUsDto GetById(int id)
        {
            var user = _unitOfWork.AboutUsRepository.GetById(id);
            return Mapper.Map<AboutUsDto>(user);
        }

        public void Create(CreateAboutUsDto item)
        {
            var user = Mapper.Map<AboutUser>(item);
            _unitOfWork.AboutUsRepository.Create(user);
            _unitOfWork.AboutUsRepository.Save();
        }

        public void Update(AboutUsDto item)
        {
            var user = Mapper.Map<AboutUser>(item);
            _unitOfWork.AboutUsRepository.Update(user);
            _unitOfWork.AboutUsRepository.Save();
        }

        public void DeleteById(int id)
        {
            _unitOfWork.AboutUsRepository.DeleteById(id);
            _unitOfWork.AboutUsRepository.Save();
        }
    }
}
