using AutoMapper;
using System.Collections.Generic;
using UserService.Interfaces;
using UserService.Dtos;
using UserService.Models;
using System.Web;
using System.IO;
using System;
using System.Linq;
using System.Drawing;
using System.Threading;

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
            var users = _unitOfWork.AboutUsRepository.GetAll().ToList();
            var usersDto = Mapper.Map<List<AboutUsDto>>(users);
            foreach (var userDto in usersDto)
                userDto.Image = GetImage(userDto.Id);
            return usersDto;
        }

        public AboutUsDto GetById(int id)
        {
            var user = _unitOfWork.AboutUsRepository.GetById(id);
            var userDto = Mapper.Map<AboutUsDto>(user);
            userDto.Image = GetImage(user.Id);
            return userDto;
        }

        public void Create(CreateAboutUsDto item, HttpFileCollectionBase files)
        {
            var user = Mapper.Map<AboutUser>(item);
            _unitOfWork.AboutUsRepository.Create(user);
            _unitOfWork.AboutUsRepository.Save();

            if(files.Count > 0)
            {
                var file = files[0];
                var fileName = GetAll().Last().Id + "." + file.FileName.Split('.').Last();
                var app = AppContext.BaseDirectory + "App_Data//Upload//";
                var path = Path.Combine(app, fileName);
                file.SaveAs(path);
            }
        }

        public void Update(AboutUsDto item, HttpFileCollectionBase files)
        {
            var user = Mapper.Map<AboutUser>(item);            
            _unitOfWork.AboutUsRepository.Update(user);
            _unitOfWork.AboutUsRepository.Save();            

            if (files.Count > 0)
            {
                
                var file = files[0];
                if(file.ContentLength > 0)
                {
                    var fileName = item.Id + "." + file.FileName.Split('.').Last();
                    var app = AppContext.BaseDirectory + "App_Data//Upload//";
                    var path = Path.Combine(app, fileName);
                    var pathDelete = Directory.GetFiles(AppContext.BaseDirectory + "App_Data//Upload//", $"{item.Id}.*"); 
                    if (pathDelete.Length > 0)
                        File.Delete(pathDelete[0]);
                    file.SaveAs(path);
                }                
            }
        }

        public void DeleteById(int id)
        {
            _unitOfWork.AboutUsRepository.DeleteById(id);
            _unitOfWork.AboutUsRepository.Save();
            var pathDelete = Directory.GetFiles(AppContext.BaseDirectory + "App_Data//Upload//", $"{id}.*");
            if (pathDelete.Length > 0)
                File.Delete(pathDelete[0]);
        }

        private byte[] GetImage(int id)
        {
            var path = Directory.GetFiles(AppContext.BaseDirectory + "App_Data//Upload//", $"{id}.*");
            var defaultPath = AppContext.BaseDirectory + "App_Data//Upload//Default.png";

            var image = new Bitmap(defaultPath, true);

            if (path.Length > 0)
            {
                image = new Bitmap(path[0], true);
            }
            var result = (byte[])new ImageConverter().ConvertTo(image, typeof(byte[]));
            image.Dispose();
            return result;
        }       
    }
}
