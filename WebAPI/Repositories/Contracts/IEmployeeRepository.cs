using WebAPI.Entities;

namespace WebAPI.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAllEmployees(bool trackchanges);
        Task<Employee> GetEmployeeByIdAsync(int id, bool trackchanges);
        Task<int> CreateEmployeeAsync(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Task<List<int>> GetSubordinatesAsync(int id, bool trackchanges);
        Task<bool> CheckEmployeeByRegistrationNumberAsync(string registrationNumber, bool trackchanges);
    }
}
