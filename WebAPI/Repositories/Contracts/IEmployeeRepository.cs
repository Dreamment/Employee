using WebAPI.Entities;

namespace WebAPI.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAllEmployees(bool trackchanges);
        Employee GetEmployeeById(int id, bool trackchanges);
        int CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        List<int> GetSubordinates(int id, bool trackchanges);
        bool CheckEmployeeByRegistrationNumber(string registrationNumber, bool trackchanges);
    }
}
