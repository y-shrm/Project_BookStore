using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_BookStore.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime OderDate { get; set; }
        public double? OrderTotal{ get; set; }


        [Required]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Street address")]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [DisplayName("Postal code")]
        public string PostalCode { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
