using Microsoft.AspNet.Identity.EntityFramework;

namespace UserService.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }

        public ApplicationRole() { }        
    }
}
