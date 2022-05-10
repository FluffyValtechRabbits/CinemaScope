using System.ComponentModel.DataAnnotations;

namespace MovieService.Entities
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}
