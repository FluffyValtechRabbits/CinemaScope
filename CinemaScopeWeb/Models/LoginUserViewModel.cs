using System.ComponentModel.DataAnnotations;

namespace CinemaScopeWeb.Models
{
    public class LoginUserViewModel
    {
        [Required]
        [Display(Name = "User name:")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }
}