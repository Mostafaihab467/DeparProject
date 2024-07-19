using DeparProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DeparProject.DBService
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Departments.Any())
                {
                    return;   // DB has been seeded
                }

                context.Departments.AddRange(
                     new Department { Name = "HR", Logo = "hr_logo.png" ,ID =  Guid.NewGuid() },
                     new Department { Name = "IT", Logo = "it_logo.png" , ID = Guid.NewGuid() },
                     new Department { Name = "Finance", Logo = "finance_logo.png" ,ID = Guid.NewGuid() },
                     new Department { Name = "Marketing", Logo = "marketing_logo.png", ID = Guid.NewGuid() },
                     new Department { Name = "Sales", Logo = "sales_logo.png" , ID = Guid.NewGuid() },
                     new Department { Name = "Customer Service", Logo = "customer_service_logo.png" ,ID = Guid.NewGuid() },
                     new Department { Name = "Research and Development", Logo = "rnd_logo.png" ,ID = Guid.NewGuid() },
                     new Department { Name = "Legal", Logo = "legal_logo.png" , ID = Guid.NewGuid() },
                     new Department { Name = "Operations", Logo = "operations_logo.png" ,ID = Guid.NewGuid() },
                     new Department { Name = "Procurement", Logo = "procurement_logo.png" , ID = Guid.NewGuid() }
                 );

                context.SaveChanges();
            }
        }
    }

}
