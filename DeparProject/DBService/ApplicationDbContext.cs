using DeparProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DeparProject.DBService
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }



        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<Department>()
               .HasMany(d => d.SubDepartments)
               .WithOne(d => d.ParentDepartment)
               .HasForeignKey(d => d.ParentDepartmentId)
              .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(builder);


        }
    }
}
