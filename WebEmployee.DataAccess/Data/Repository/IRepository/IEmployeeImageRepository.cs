
using WebEmployee.Models.Models;

namespace WebEmployee.DataAccess.Data.Repository.IRepository
{
    public interface IEmployeeImageRepository : IRepository<EmployeeImage>
    {
        Task UpdateAsync(EmployeeImage obj);

    }
}
