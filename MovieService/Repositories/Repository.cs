using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;
using MovieService.Contexts;
using MovieService.Interfaces;

namespace MovieService.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected MovieContext _context;

        public Repository(MovieContext context)
        {
            _context = context;
        }
        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public void Update(T item)
        {
            _context.Set<T>().AddOrUpdate(item);
        }

        public void DeleteById(int id)
        {
            var item = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(item);
        }

        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public IEnumerable<T>  GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
