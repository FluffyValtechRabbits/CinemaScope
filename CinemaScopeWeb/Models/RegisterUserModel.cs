using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaScopeWeb.Models
{
    public class RegisterUserModel
    {
        [Required]
        public string FisrtName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Something went wrong.")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}