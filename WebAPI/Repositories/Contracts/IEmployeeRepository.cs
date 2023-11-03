using WebAPI.Entities;

namespace WebAPI.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAllEmployees(bool trackchanges);
        Task<Employee> GetEmployeeById(int id, bool trackchanges);
        int CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Task<List<int>> GetSubordinates(int id, bool trackchanges);
        Task<bool> CheckEmployeeByRegistrationNumber(string registrationNumber, bool trackchanges);
    }
}
