using ApplicationCore.Auto_Mapper;
using ApplicationCore.DomainServices;
using ApplicationCore.Interfaces;
using EF_layer;
using EF_layer.Model;
using Infrastructure.Services;
using Library_Managment_System.config;
using Library_Managment_System.Models;
using Library_Managment_System.Seeders;
using Library_Managment_System.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RepositoryLayer.Interfaces;


namespace Library_Managment_System
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(Options => Options.UseLazyLoadingProxies()
            .UseSqlServer(builder.Configuration.GetConnectionString("constr"),
            x=>x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(RepositoryEf<>));
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IBookService,BookService>();
            builder.Services.AddScoped<IMemberService,MemberService>();
            builder.Services.AddScoped<IBorrowService,BorrowService>();
            builder.Services.AddScoped<IIdentitySeeder, IdentitySeeder>();
            builder.Services.AddScoped<IAccountService,AccountService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
           

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(
                Options=> {
                    Options.Password.RequireDigit = false;
                    Options.Password.RequireNonAlphanumeric = false;
                    Options.Password.RequireUppercase = false;

                 } ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.ConfigureApplicationCookie(Options => {
                Options.ExpireTimeSpan = TimeSpan.FromDays(14);
                Options.SlidingExpiration = true;
                Options.LoginPath = "/Account/logIn";
                });
            var app = builder.Build();

            using (var scop = app.Services.CreateScope())
            {
                var service = scop.ServiceProvider;
                var seeder = service.GetRequiredService<IIdentitySeeder>();
                await seeder.SeedRolesAndAdminAsync(service);
            }

            app.UseHttpsRedirection();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }


}
