using WebAPI.Entities;

namespace WebAPI.Services.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees(bool trackChanges);
        Employee GetEmployeeById(int id, bool trackChanges);
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee, bool trackChanges);
        bool DeleteEmployee(int id, bool trackChanges);
    }
}
