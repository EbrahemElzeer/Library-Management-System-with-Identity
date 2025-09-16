using ApplicationCore.DTOS;
using EF_layer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library_Managment_System.View_Model
{
    public class AssignRoleViewModel
    {
        public string SelectedUserId { get; set; }
        public string SelectedRoleName { get; set; }

        public List<UserDto> users { get; set; } = new();
        public List<RoleDto> roles { get; set; }= new();

        public IEnumerable<SelectListItem> UsersSelectList =>
       users.Select(u => new SelectListItem { Value = u.UserId, Text = u.UserName });

        public IEnumerable<SelectListItem> RolesSelectList =>
            roles.Select(r => new SelectListItem { Value = r.RoleName, Text = r.RoleName });

    }
}
