using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOS;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllRolesAsync();
        Task  DeleteRoleName(string roleName);

        Task<List<UserDto>>GetAllUsersAsync();

        Task<IdentityResult> AssignRoleToUserAsync(AssignRoleDto assignRoleDto);

        Task<List<UserWithRoleDto>> UsersWithRoles();

        Task<(bool Success, string ErrorMessage)> DeleteUserAsync(string userId);
    }
}
