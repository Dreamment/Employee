using System.Diagnostics.Eventing.Reader;
using WebAPI.Entities;
using WebAPI.Repositories.Contracts;
using WebAPI.Services.Contracts;

namespace WebAPI.Services
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;

        public EmployeeManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public bool CreateEmployee(Employee employee)
        {
            if (employee == null)
                return false;

            _repositoryManager.Employee.CreateEmployee(employee);
            _repositoryManager.Save();
            return true;
        }

        public bool DeleteEmployee(int id, bool trackChanges)
        {
            var employeeToDelete = GetEmployeeById(id, trackChanges);
            if (employeeToDelete == null)
                return false;
            _repositoryManager.Employee.DeleteEmployee(employeeToDelete);
            _repositoryManager.Save();
            return true;

        }

        public IEnumerable<Employee> GetAllEmployees(bool trackChanges) 
            => _repositoryManager.Employee.GetAllEmployees(trackChanges);

        public Employee GetEmployeeById(int id, bool trackChanges) 
            => _repositoryManager.Employee.GetEmployeeById(id, trackChanges);

        public bool UpdateEmployee(Employee employee, bool trackChanges)
        {
            var employeeToUpdate = GetEmployeeById(employee.Id, trackChanges);
            if (employeeToUpdate == null)
                return false;
            _repositoryManager.Employee.UpdateEmployee(employee);
            _repositoryManager.Save();
            return true;
        }
    }
}
