using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MovieService.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(220)]
        public string Title { get; set; }

        public Bitmap Poster { get; set; }

        [Required]
        [Range(1895,2030)]
        public int Year { get; set; }

        [Required]
        public int TypeId { get; set; }

        public virtual Type Type { get; set; }

        public string Cast { get; set; }

        public string Plot { get; set; }

        public int? Budget { get; set; }

        public int? BoxOffice { get; set; }

        public double? RatingIMDb { get; set; }

        public double? SiteUsersRating { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
