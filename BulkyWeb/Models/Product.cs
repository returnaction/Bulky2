﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }

    }
}
