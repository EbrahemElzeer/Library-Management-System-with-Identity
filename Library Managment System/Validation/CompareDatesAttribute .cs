using System.ComponentModel.DataAnnotations;
using Library_Managment_System.View_Model;

namespace Library_Managment_System.Validation
{
    public class CompareDatesAttribute:ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
         

            var borrowViewModel = (BorrowViewModel)validationContext.ObjectInstance;

            if (borrowViewModel.BorrowDate >= borrowViewModel.ReturnDate)
            {
                return new ValidationResult("The borrowing date must precede the return date.");
            }
            return ValidationResult.Success;
        }
    }
}
