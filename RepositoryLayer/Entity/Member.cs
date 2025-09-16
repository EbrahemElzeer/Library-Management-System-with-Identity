using System.ComponentModel.DataAnnotations;

namespace Library_Managment_System.Models
{
    public class Member
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Reqiured")]
        [StringLength(50, MinimumLength=3,ErrorMessage = "Title must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Range(1000000000, 9999999999, ErrorMessage = "Enter a valid 10-digit phone number.")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }
    }
}
