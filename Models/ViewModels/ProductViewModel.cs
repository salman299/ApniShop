using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Models.ViewModels
{
    public class ProductViewModel
    {

        public int? ID { get; set; }

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

        [Display(Name ="Deploy Status")]
        public bool IsDeploy { get; set; }

        [Display(Name = "Approved?")]
        public bool IsAdminApproved { get; set; }

        [Display(Name = "Created By")]
        public string UserName { get; set; }

        [Display(Name = "Approved By")]
        public string AdminName { get; set; }


    }
}
