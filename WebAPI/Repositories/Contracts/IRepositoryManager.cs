namespace WebAPI.Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IEmployeeRepository Employee { get; }
        Task SaveAsync();
    }
}
