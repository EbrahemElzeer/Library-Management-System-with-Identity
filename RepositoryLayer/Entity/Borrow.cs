using System.ComponentModel.DataAnnotations;

namespace Library_Managment_System.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "تاريخ الإرجاع مطلوب.")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate {  get; set; }
        [Required(ErrorMessage = "تاريخ الاستعارة مطلوب.")]
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
