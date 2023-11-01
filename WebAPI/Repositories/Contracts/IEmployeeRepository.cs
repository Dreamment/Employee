using WebAPI.Entities;

namespace WebAPI.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAllEmployees(bool trackchanges);
        Employee GetEmployeeById(int id, bool trackchanges);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
