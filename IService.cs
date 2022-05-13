using System.Collections.Generic;

namespace UserService.Interfaces
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(string id);

        T GetByEmail(string email);

        void AddUser(T user);

        void DeleteUser(T user);
    }
}
