using Microsoft.EntityFrameworkCore;
using WebEmployee.web.Models;

namespace WebEmployee.web.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeImage> EmployeeImagess { get; set; }

    }
}
