using System.ComponentModel.DataAnnotations;

namespace Library_Managment_System.View_Model
{
    public class LoginUserViewModel
    {

        public string Name { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me!")]
        public bool RememberME { get; set; }
    }
}
