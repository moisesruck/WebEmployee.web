namespace WebEmployee.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        IEmployeeImageRepository EmployeeImagee { get; }
        IEmployeeTypeRepository EmployeeType { get; }
        Task SaveAsync();
    }
}
