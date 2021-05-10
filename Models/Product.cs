using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string UserID { get; set; }

        public string AdminID { get; set; }

        public bool IsDeployed { get; set; }

        public bool IsAdminApproved { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser UserCustomer { get; set; }

        [ForeignKey("AdminID")]
        public ApplicationUser UserAdmin { get; set; }

    }
}
