using WebEmployee.Models.Models;

namespace WebEmployee.DataAccess.Data.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task UpdateAsync(Employee obj);
    }
}
