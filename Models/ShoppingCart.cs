using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Project_BookStore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_BookStore.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
        [Range(1,10, ErrorMessage ="Please eanter a value between 1 and 10")]
        public int Count { get; set; }

        public string ApplicationUsderId { get; set; }
        [ForeignKey("ApplicationUsderId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public double? Price { get; set; }


    }
}
