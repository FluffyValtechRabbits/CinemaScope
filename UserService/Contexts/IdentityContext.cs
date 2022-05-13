using Microsoft.AspNet.Identity.EntityFramework;
using UserService.Models;

namespace UserService.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext() : base("IdentityDbContext") { }

        public static IdentityContext Create()
        {            
            return new IdentityContext();
        }
    }
}
