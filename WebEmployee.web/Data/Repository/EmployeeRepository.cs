using WebEmployee.web.Data.Repository.IRepository;
using WebEmployee.web.Models;

namespace WebEmployee.web.Data.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Employee obj)
        {
                
            var objFromdb = _db.Employees.FirstOrDefault(u => u.Id == obj.Id);

            if (objFromdb != null)
            {
                objFromdb.Name = obj.Name;
                objFromdb.Lastname = obj.Lastname;
                objFromdb.DateofBirth = obj.DateofBirth;
                objFromdb.NationalId = obj.NationalId;
                objFromdb.EmployeeBadge = obj.EmployeeBadge;
                objFromdb.BirthPlaceCity = obj.BirthPlaceCity;
                objFromdb.BirthPlaceCountry = obj.BirthPlaceCountry;
                objFromdb.CreationTime = obj.CreationTime;


            }

        }
    }
}
