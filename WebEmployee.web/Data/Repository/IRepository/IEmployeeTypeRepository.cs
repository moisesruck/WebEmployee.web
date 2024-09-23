using WebEmployee.web.Models;

namespace WebEmployee.web.Data.Repository.IRepository
{
    public interface IEmployeeTypeRepository : IRepository<EmployeeType>
    {
        void Update(EmployeeType obj);
    }
}
