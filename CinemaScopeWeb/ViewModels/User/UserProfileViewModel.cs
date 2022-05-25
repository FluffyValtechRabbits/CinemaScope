using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MovieService.Dtos;

namespace CinemaScopeWeb.ViewModels
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

        public List<UserStatsMovieDto> WatchedMovies { get; set; }

        public List<UserStatsMovieDto> LikedMovies { get; set; }

        public List<UserStatsMovieDto> DislikedMovies { get; set; }

        
    }
}