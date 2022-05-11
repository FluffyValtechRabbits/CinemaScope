using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using UserService.Models;

namespace UserService.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext() : base("IdentityDbContext") 
        {
            
        }

        public static IdentityContext Create()
        {            
            return new IdentityContext();
        }
    }
}
