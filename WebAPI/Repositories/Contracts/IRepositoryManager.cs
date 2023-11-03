namespace WebAPI.Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IEmployeeRepository Employee { get; }
        Task Save();
    }
}
