using System;
using System.Collections.Generic;

namespace UserService.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(string id);

        T GetByEmail(string email);

        //void Create(T entity);

        void Create(T entity, string password);

        void Update(T entity);

        void DeleteById(string id);

        void Save();
    }
}
