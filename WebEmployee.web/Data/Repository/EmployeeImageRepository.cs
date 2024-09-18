using WebEmployee.web.Data.Repository.IRepository;
using WebEmployee.web.Models;

namespace WebEmployee.web.Data.Repository
{
    public class EmployeeImageRepository : Repository<EmployeeImage>, IEmployeeImageRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(EmployeeImage obj)
        {
            _db.EmployeeImagess.Update(obj);


        }
    }
}
