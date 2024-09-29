using WebEmployee.web.Models;

namespace WebEmployee.web.Data.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task UpdateAsync(Employee obj);
    }
}
