using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage ="The {0} must be atleast {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }


        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="The Password and Confirmation Password doesn't match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name="Role Name")]
        public string RoleName { get; set; }


    }
}
