using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using UserService.Models;

namespace UserService.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AboutUser> AboutUsers { get; set; }

        public IdentityContext() : base("IdentityDbContext") { }

        public static IdentityContext Create()
        {            
            return new IdentityContext();
        }
    }
}
