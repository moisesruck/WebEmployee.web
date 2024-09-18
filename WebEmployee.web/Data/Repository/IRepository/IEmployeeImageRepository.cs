using WebEmployee.web.Models;

namespace WebEmployee.web.Data.Repository.IRepository
{
    public interface IEmployeeImageRepository : IRepository<EmployeeImage>
    {
        void Update(EmployeeImage obj);
    }
}
