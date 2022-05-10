using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieService.Contexts;
using MovieService.Interfaces;
using MovieService.Entities;

namespace MovieService
{
    public class UnitOfWork : IUnitOfWork
    {
        private MovieContext _context;

        public UnitOfWork(MovieContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
