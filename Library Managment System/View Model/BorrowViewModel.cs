using System.ComponentModel.DataAnnotations;
using Library_Managment_System.Models;
using Library_Managment_System.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Library_Managment_System.View_Model
{
    public class BorrowViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "The Book Is Required")]
        public int? BookID { get; set; }
        [Required(ErrorMessage = "The Member Is Required")]
        public int? MemberID { get; set; }

        [BindNever]
        public string? BookTitle { get; set; }

        [BindNever]
        public string? MemberName { get; set; }
        [Required(ErrorMessage = "Borrow date required.")]
        [CompareDates(ErrorMessage = "the borrowing date must precede the return date..")]
        [DataType(DataType.Date)]
        public DateTime BorrowDate { get; set; }
        [Required(ErrorMessage = "Return date required.")]
        
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        
        public List<Books> BookList { get; set; } = new();   
        public List<Member> MemberList { get; set; } = new();
        public bool IsValidDateRange => BorrowDate < ReturnDate;

    }
}