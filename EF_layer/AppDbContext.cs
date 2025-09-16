using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_layer.Model;
using Library_Managment_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EF_layer
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Books> Books { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<Member> Member { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Books>().ToTable("Books");

            modelBuilder.Entity<IdentityUser>()
                .HasIndex(u => u.Email)
                  .IsUnique();

        }
    }
}
