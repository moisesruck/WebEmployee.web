using WebEmployee.Models.Models;

namespace WebEmployee.DataAccess.Data.Repository.IRepository
{
    public interface IEmployeeTypeRepository : IRepository<EmployeeType>
    {
        Task UpdateAsync(EmployeeType obj);
    }
}
