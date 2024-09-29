using WebEmployee.web.Models;

namespace WebEmployee.web.Data.Repository.IRepository
{
    public interface IEmployeeTypeRepository : IRepository<EmployeeType>
    {
        Task UpdateAsync(EmployeeType obj);
    }
}
