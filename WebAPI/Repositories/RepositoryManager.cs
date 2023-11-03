using WebAPI.Repositories.Contracts;

namespace WebAPI.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private Lazy<IEmployeeRepository> _employeeRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_repositoryContext));
        }

        public IEmployeeRepository Employee => _employeeRepository.Value;

        public async Task Save() => await _repositoryContext.SaveChangesAsync();
    }
}
