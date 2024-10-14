using Microsoft.EntityFrameworkCore;
using WebEmployee.Models.Models;

namespace WebEmployee.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeImage> EmployeeImagess { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }

    }
}
