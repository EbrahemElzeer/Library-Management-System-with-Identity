using System.ComponentModel.DataAnnotations;

namespace Library_Managment_System.Models
{
    public class Books
    {

        public int Id { get; set; }

        [Required (ErrorMessage ="Title Is Reqiured")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "ِAuther Is Reqiured")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Title must be between 3 and 100 characters.")]
        public string Auther { get; set; }

        [Display(Name = "Published Date")]
        [Required(ErrorMessage = "Published date is required.")]
        [DataType(DataType.Date)]
        public DateTime PublishedDate {  get; set; }

        [Display(Name = "Number Of Copies")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of copies must be at least 1.")]
        public int NumberOfCopies { get; set; }


    }
}
