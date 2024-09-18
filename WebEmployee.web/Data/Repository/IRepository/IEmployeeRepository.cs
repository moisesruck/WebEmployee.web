using WebEmployee.web.Models;

namespace WebEmployee.web.Data.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        void Update(Employee obj);
    }
}
