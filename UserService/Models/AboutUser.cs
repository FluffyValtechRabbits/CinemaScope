using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class AboutUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        //public string Image { get; set; }
    }
}
