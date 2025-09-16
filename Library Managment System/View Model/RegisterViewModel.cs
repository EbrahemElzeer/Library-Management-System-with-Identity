using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Library_Managment_System.View_Model
{
    public class RegisterViewModel
    {
        [Required]
        [Unicode]
        public string Email { get; set; }

        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "The {0} must be at least {1} and at most {2} characters long.")]

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }


    }
}
