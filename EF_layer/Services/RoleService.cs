using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOS;
using ApplicationCore.Interfaces;
using AutoMapper;
using EF_layer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        public RoleService(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }


        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            var roles = await roleManager.Roles
          .Select(r => new RoleDto { RoleId = r.Id, RoleName = r.Name })
          .ToListAsync();



            return roles;

        }
        public async Task DeleteRoleName(string roleName)
        {

            var Role = await roleManager.FindByNameAsync(roleName);
            if (Role != null)
            {
                await roleManager.DeleteAsync(Role);
            }


        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var user = await userManager.Users.ToListAsync();
            return user.Select(u => new UserDto
            {
                UserId = u.Id,
                Email = u.Email,
                UserName = u.UserName
            }).ToList();

        }

        public async Task<IdentityResult> AssignRoleToUserAsync(AssignRoleDto assignRoleDto)
        {
            var user = await userManager.FindByIdAsync(assignRoleDto.UserId);
            if (user == null || string.IsNullOrEmpty(assignRoleDto.RoleName))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Invalid user or role."
                });

            }
            return await userManager.AddToRoleAsync(user, assignRoleDto.RoleName);

        }

        public async Task<List<UserWithRoleDto>> UsersWithRoles()
        {
            var users = await userManager.Users.ToListAsync();
            var result = new List<UserWithRoleDto>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);

                var dto = mapper.Map<UserWithRoleDto>(user) with
                {
                    RoleName = roles.FirstOrDefault() ?? "No Role"
                };

                result.Add(dto);
            }

            return result;
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            if (await userManager.IsInRoleAsync(user, "SuperAdmin"))
            {
                var superAdmins = await userManager.GetUsersInRoleAsync("SuperAdmin");
                if (superAdmins.Count <= 1)
                {
                    return (false, "Cannot delete the last SuperAdmin.");
                }
            }

            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                var admins = await userManager.GetUsersInRoleAsync("Admin");
                if (admins.Count <= 1)
                {
                    return (false, "Cannot delete the last Admin.");
                }
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return (false, "Failed to delete user.");
            }

            return (true, null);
        }
    }
}
