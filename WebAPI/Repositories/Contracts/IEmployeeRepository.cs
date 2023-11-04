using WebAPI.Entities;

namespace WebAPI.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackchanges);
        Task<Employee> GetEmployeeByIdAsync(int id, bool trackchanges);
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Employee employee);
        Task<List<int>> GetSubordinatesAsync(int id, bool trackchanges);
        Task<bool> CheckEmployeeByRegistrationNumberAsync(string registrationNumber, bool trackchanges);
    }
}
