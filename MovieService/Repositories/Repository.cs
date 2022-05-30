using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using MovieService.Interfaces.RepositoryInterfaces;

namespace MovieService.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }
        public virtual void Add(T item)
        {
            _context.Set<T>().Add(item);
            Save();
        }

        public virtual void Update(T item)
        {
            _context.Set<T>().AddOrUpdate(item);
            Save();
        }

        public virtual void DeleteById(int id)
        {
            var item = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(item);
            Save();
        }

        public virtual void Delete(T item)
        {
            _context.Set<T>().Remove(item);
            Save();
        }

        public virtual IEnumerable<T>  GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
