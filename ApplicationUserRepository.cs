using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using UserService.Interfaces;
using UserService.Models;
using UserService.Contexts;
using UserService.Managers;
using System;

namespace UserService.Repositories
{
    public class ApplicationUserRepository : IRepository<ApplicationUser>
    {
        private readonly IdentityContext _context;
        private readonly ApplicationUserManager _userManager;

        public ApplicationUserRepository(ApplicationUserManager userManager)
        {
            _userManager = userManager;
            _context = new IdentityContext();
        }

        //public void Create(ApplicationUser entity)
        //{
        //    _userManager.Create(entity);
        //}

        public void Create(ApplicationUser entity, string password)
        {
            //_userManager.Create(entity, password);
            _userManager.CreateAsync(entity, password);
        }

        public void DeleteById(string id)
        {
            var user = _userManager.FindById(id);
            if(user != null)
                _context.Users.Remove(user);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Users;
        }

        public ApplicationUser GetByEmail(string email)
        {
            return _userManager.FindByEmail(email);
            //return _context.Users.FirstOrDefault(u => u.Email.Equals(email));
        }

        public ApplicationUser GetById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id.Equals(id));
        }

        public void Update(ApplicationUser entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
