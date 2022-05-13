using System.ComponentModel.DataAnnotations;

namespace CinemaScopeWeb.ViewModels
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