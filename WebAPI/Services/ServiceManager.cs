using WebAPI.Repositories.Contracts;
using WebAPI.Services.Contracts;

namespace WebAPI.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmployeeService> _employeeService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeManager(repositoryManager));
        }

        public IEmployeeService Employee => _employeeService.Value;
    }
}
