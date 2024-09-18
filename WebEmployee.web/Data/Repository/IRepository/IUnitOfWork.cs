namespace WebEmployee.web.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {

        IEmployeeRepository Employee { get; }

       
        IEmployeeImageRepository EmployeeImagee { get; }

      

        void Save();
    }
}
