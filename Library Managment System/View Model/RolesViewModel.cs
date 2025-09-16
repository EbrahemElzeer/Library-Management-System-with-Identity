using System.ComponentModel.DataAnnotations;

namespace Library_Managment_System.View_Model
{
    public class RolesViewModel
    {
        public string RoleId { get; set; }
        [Required(ErrorMessage = "Role is Required")]
        [Display(Name = "Role Name")]
        public string RoleName {  get; set; }
    }
}
