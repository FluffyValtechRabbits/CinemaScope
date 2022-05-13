using UserService.Contexts;
using UserService.Interfaces;
using UserService.Models;
using UserService.Repositories;
using UserService.Managers;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Host.SystemWeb;
using System.Web;

namespace UserService
{
    public class UnitOfWork : IUnitOfWork
    {
        private IdentityContext _context;
        private ApplicationUserManager _userManager;

        private IRepository<ApplicationUser> _userRepository;

        public UnitOfWork()
        {
            _context = new IdentityContext();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
        }
                

        public IRepository<ApplicationUser> UserRepository
        {
            get { return _userRepository is null ? 
                    new ApplicationUserRepository(UserManager) : 
                    _userRepository; }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager is null ? new ApplicationUserManager(new UserStore<ApplicationUser>(_context)) : _userManager;
            }
        }
    }
}
