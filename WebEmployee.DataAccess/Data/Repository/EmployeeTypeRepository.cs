using WebEmployee.DataAccess.Data.Repository.IRepository;
using WebEmployee.Models.Models;

namespace WebEmployee.DataAccess.Data.Repository
{
    public class EmployeeTypeRepository : Repository<EmployeeType>, IEmployeeTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(EmployeeType obj)
        {
            _db.EmployeeTypes.Update(obj);
            await _db.SaveChangesAsync();
        }

    }
}
