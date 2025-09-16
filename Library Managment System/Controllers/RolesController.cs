using ApplicationCore.DTOS;
using ApplicationCore.Interfaces;
using AutoMapper;
using EF_layer.Model;
using Library_Managment_System.View_Model;
using MailKit.Net.Imap;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library_Managment_System.Controllers
{
//    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager
            , IRoleService roleService, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.roleService = roleService;
            this.mapper = mapper;
        }
     
          public async Task<IActionResult> Index()
          {
            var roles = await roleService.GetAllRolesAsync();
            var roleMap = mapper.Map<List<RolesViewModel>>(roles);

            return View("Roles", roleMap);
          }
      
       
        public async Task<IActionResult> Delete (string RoleName)
        {
            if (RoleName == "SuperAdmin")
            {
                TempData["ErrorMessage"] = "Cannot delete the SuperAdmin role.";
                return RedirectToAction("Index");
            }

            await roleService.DeleteRoleName(RoleName);
            return RedirectToAction("Index");
        }



        public async Task <IActionResult> AssignRole()
        {

            var users = await roleService.GetAllUsersAsync(); 
            var roles = await roleService.GetAllRolesAsync(); 

            var model = new AssignRoleViewModel
            {
                users = users,
                roles = roles
            };
            return View("AssignRole", model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
        {
            Console.WriteLine($"User: {model.SelectedUserId}, Role: {model.SelectedRoleName}");


            if (!string.IsNullOrEmpty(model.SelectedUserId) && !string.IsNullOrEmpty(model.SelectedRoleName))
            {
                var  map =  mapper.Map<AssignRoleDto>(model);
                //var map = new AssignRoleDto
                //{
                //    UserId = model.SelectedUserId,
                //    RoleName = model.SelectedRoleName
                //};
                var result = await roleService.AssignRoleToUserAsync(map);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var Erorr in result.Errors)
                    {
                        ModelState.AddModelError("", Erorr.Description);
                    }
                }


            }
            model.users = await roleService.GetAllUsersAsync();
            model.roles = await roleService.GetAllRolesAsync();

            return View("AssignRole", model);

        }

        public async Task<IActionResult> UsersWithRoles()
        {

            var UserWithRoles = await roleService.UsersWithRoles();

            //return View("UsersWithRoles", UserWithRoles);
            var viewModels = mapper.Map<List<UserWithRoleViewModel>>(UserWithRoles);
            return View(viewModels);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string UserId)
        {
            Console.WriteLine($"Deleting user with ID: {UserId}");

            if (string.IsNullOrEmpty(UserId))
            {
                TempData["ErrorMessage"] = "User ID is null or empty.";
                return RedirectToAction("UsersWithRoles");
            }
            var (success, errorMessage) = await roleService.DeleteUserAsync(UserId);

            if (!success && !string.IsNullOrEmpty(errorMessage))
            {
                TempData["ErrorMessage"] = errorMessage;
            }

            return RedirectToAction("UsersWithRoles");
        }

    }
}
