using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        [Required]
        public string ISBN { get; set; } = null!;
        [Required]
        public string Author { get; set; } = null!;

        [Required]
        public double ListPrice { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Price50 { get; set; }
        [Required]
        public double Price100 { get; set; }

    }
}
