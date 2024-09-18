using WebEmployee.web.Data.Repository.IRepository;

namespace WebEmployee.web.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;


        public IEmployeeRepository Employee { get; private set; }

      
        public IEmployeeImageRepository EmployeeImagee { get; private set; }
     


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            

            Employee = new EmployeeRepository(_db);

           

            EmployeeImagee = new EmployeeImageRepository(_db);

          


        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
