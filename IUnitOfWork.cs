using System;
using UserService.Managers;
using UserService.Models;

namespace UserService.Interfaces
{
    public interface IUnitOfWork 
    {
        ApplicationUserManager UserManager { get; }


        IRepository<ApplicationUser> UserRepository { get; }
    }
}
