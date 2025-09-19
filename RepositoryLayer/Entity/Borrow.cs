using System.ComponentModel.DataAnnotations;

namespace Library_Managment_System.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Return Date Is Required")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate {  get; set; }
        [Required(ErrorMessage = "The Borrow Date Is Required")]
        [DataType(DataType.Date)]
        public DateTime BorrowDate {  get; set; }

        public virtual Books? Book { get; set; }
        [Required(ErrorMessage = "The Book Is Required")]
        public int BookID { get; set; }

        public virtual Member? Member { get; set; }
        [Required(ErrorMessage = "The Member Is Required")]
        public  int MemberID { get; set; }

    }
}
