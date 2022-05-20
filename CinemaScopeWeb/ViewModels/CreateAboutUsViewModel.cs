using System.ComponentModel.DataAnnotations;

namespace CinemaScopeWeb.ViewModels
{
    public class CreateAboutUsViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name:")]
        [RegularExpression(@"^(?!.*\s\s)(?!.*\.\.)(?!.*,,)[A-Z][a-zA-Z .,]{2,60}$",
            ErrorMessage = "Name is not correct.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        //public string Image { get; set; }
    }
}