using System.Collections.Generic;
using System.Web;
using System.Linq;
using AutoMapper;
using Identity.Interfaces;
using Identity.Dtos;
using Identity.Models;

namespace Identity.Services
{
    public class AboutUsService : IAboutUsService
    {
        private IUnitOfWork _unitOfWork;
        private IImageService _imageService;

        public AboutUsService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        public IEnumerable<AboutUsDto> GetAll()
        {
            var users = _unitOfWork.AboutUsRepository.GetAll().ToList();
            var usersDto = Mapper.Map<List<AboutUsDto>>(users);
            foreach (var userDto in usersDto)
                userDto.Image = _imageService.GetImage(userDto.Id);
            return usersDto;
        }

        public AboutUsDto GetById(int id)
        {
            var user = _unitOfWork.AboutUsRepository.GetById(id);
            var userDto = Mapper.Map<AboutUsDto>(user);
            userDto.Image = _imageService.GetImage(user.Id);
            return userDto;
        }

        public void Create(CreateAboutUsDto item, HttpFileCollectionBase files)
        {
            var user = Mapper.Map<AboutUser>(item);
            _unitOfWork.AboutUsRepository.Create(user);
            _unitOfWork.Save();
            var id = GetAll().Last().Id;
            _imageService.CreateImage(id, files);            
        }

        public void Update(AboutUsDto item, HttpFileCollectionBase files)
        {
            var user = Mapper.Map<AboutUser>(item);            
            _unitOfWork.AboutUsRepository.Update(user);
            _unitOfWork.Save();
            _imageService.UpdateImage(item.Id, files);
        }

        public void DeleteById(int id)
        {
            _unitOfWork.AboutUsRepository.DeleteById(id);
            _unitOfWork.Save();
            _imageService.DeleteImage(id);
        }            
    }
}
