using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Project_BookStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(90)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        //[Display(Name = "List Price")]
        [Range(100, 2000)]
        public double? Price { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }

        
        public string ImageURL { get; set; }


    }
}
