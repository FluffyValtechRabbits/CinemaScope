using System.Collections.Generic;
using UserService.Models;

namespace UserService.Interfaces
{
    public interface IAboutUsRepository
    {
        IEnumerable<AboutUser> GetAll();

        AboutUser GetById(int id);

        void Create(AboutUser item);

        void Update(AboutUser item);

        void DeleteById(int id);

        void Save();
    }
}
