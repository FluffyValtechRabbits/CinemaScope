using System.Collections.Generic;
using UserService.Dtos;

namespace UserService.Interfaces
{
    public interface IAboutUsService
    {
        IEnumerable<AboutUsDto> GetAll();

        AboutUsDto GetById(int id);

        void Create(CreateAboutUsDto item);

        void Update(AboutUsDto item);

        void DeleteById(int id);
    }
}
