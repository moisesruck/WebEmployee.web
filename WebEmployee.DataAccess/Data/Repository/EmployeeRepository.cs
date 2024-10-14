using Microsoft.EntityFrameworkCore;
using WebEmployee.DataAccess.Data.Repository.IRepository;
using WebEmployee.Models.Models;

namespace WebEmployee.DataAccess.Data.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(Employee obj)
        {
            var objFromDb = await _db.Employees.FirstOrDefaultAsync(u => u.Id == obj.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Lastname = obj.Lastname;
                objFromDb.DateofBirth = obj.DateofBirth;
                objFromDb.NationalId = obj.NationalId;
                objFromDb.EmployeeBadge = obj.EmployeeBadge;
                objFromDb.BirthPlaceCity = obj.BirthPlaceCity;
                objFromDb.BirthPlaceCountry = obj.BirthPlaceCountry;
                objFromDb.CreationTime = obj.CreationTime;
                objFromDb.EmployeeTypeId = obj.EmployeeTypeId;
                objFromDb.BossId = obj.BossId;
            }
        }
    }
}
