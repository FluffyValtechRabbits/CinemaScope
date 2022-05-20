using UserService.Interfaces;
using UserService.Contexts;
using UserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;

namespace UserService.Repositories
{
    public class AboutUsRepository : IAboutUsRepository
    {
        private IdentityContext _context;

        public AboutUsRepository(IdentityContext context)
        {
            _context = context;
        }

        public void Create(AboutUser item)
        {
            if(item is null)
                throw new ArgumentNullException("User cannot be null.");
            if(String.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentNullException("User's name cannot be null or empty.");
            if(String.IsNullOrWhiteSpace(item.Description))
                throw new ArgumentNullException("User's description cannot be null or empty.");            

            _context.AboutUsers.Add(item);
        }

        public void Update(AboutUser item)
        {
            if (item is null)
                throw new ArgumentNullException("User cannot be null.");
            if (String.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentNullException("User's name cannot be null or empty.");
            if (String.IsNullOrWhiteSpace(item.Description))
                throw new ArgumentNullException("User's description cannot be null or empty.");

            _context.AboutUsers.AddOrUpdate(item);
        }

        public void DeleteById(int id)
        {
            var user = _context.AboutUsers.FirstOrDefault(x => x.Id == id);
            if(user != null)
                _context.AboutUsers.Remove(user);
        }

        public IEnumerable<AboutUser> GetAll()
        {  
            return _context.Set<AboutUser>().ToList();
        }

        public AboutUser GetById(int id)
        {            
            return _context.Set<AboutUser>().FirstOrDefault(u => u.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }        
    }
}
