using System.ComponentModel.DataAnnotations;

namespace CinemaScopeWeb.Models
{
    public class UserProfileViewModel
    {
        [Required]
        [Display(Name = "Your first name:")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Your last name:")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Your user name on the site:")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Email:")]
        public string Email { get; set; }
    }
}