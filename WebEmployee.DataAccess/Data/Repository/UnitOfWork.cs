using WebEmployee.DataAccess.Data.Repository.IRepository;

namespace WebEmployee.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;

        public IEmployeeRepository Employee { get; private set; }
        public IEmployeeImageRepository EmployeeImagee { get; private set; }
        public IEmployeeTypeRepository EmployeeType { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Employee = new EmployeeRepository(_db);
            EmployeeImagee = new EmployeeImageRepository(_db);
            EmployeeType = new EmployeeTypeRepository(_db);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
