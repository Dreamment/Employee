using AutoMapper;
using WebAPI.Repositories.Contracts;
using WebAPI.Services.Contracts;

namespace WebAPI.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmployeeService> _employeeService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeManager(repositoryManager, mapper));
        }

        public IEmployeeService Employee => _employeeService.Value;
    }
}
