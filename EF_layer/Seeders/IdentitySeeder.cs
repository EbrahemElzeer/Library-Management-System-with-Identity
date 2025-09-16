using ApplicationCore.Interfaces;
using EF_layer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Library_Managment_System.Seeders
{
    public class IdentitySeeder:IIdentitySeeder
    {


        public  async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManger = serviceProvider.GetRequiredService<RoleManager< IdentityRole >> ();
            var userManger = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roles = new[] { "SuperAdmin", "Admin" };
            foreach(var role in roles)
            {
                if (!await roleManger.RoleExistsAsync(role))
                {
                    await roleManger.CreateAsync(new IdentityRole(role));

                }
            }
            var SuperAdminName = "SuperAdmin";
            var SuperAdminPassword = "SuperAdmin";
            var existingUser = await userManger.FindByNameAsync(SuperAdminName);
            if (existingUser == null) {

                var superAdminUser = new ApplicationUser
                {
                    UserName = SuperAdminName,
                    
                };

                var result = await userManger.CreateAsync(superAdminUser, SuperAdminPassword);

               
                if (result.Succeeded)
                {
                    
                    await userManger.AddToRoleAsync(superAdminUser, "SuperAdmin");
                }
                else
                {
                    
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }
            else
            {
                Console.WriteLine("SuperAdmin user already exists.");


            }

        }
    }
}
