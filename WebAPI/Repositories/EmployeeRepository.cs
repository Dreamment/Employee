using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Repositories.Contracts;

namespace WebAPI.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context)
        {

        }

        public async Task<bool> CheckEmployeeByRegistrationNumberAsync(string registrationNumber, bool trackchanges)
        {
            var employee = await FindByCondition(e => e.RegistrationNumber.Equals(registrationNumber), trackchanges).SingleOrDefaultAsync();
            if (employee == null)
                return false;
            return true;
        }

        public async Task<int> CreateEmployeeAsync(Employee employee)
        {
            Create(employee);
            return employee.Id;
        }

        public void DeleteEmployee(Employee employee)
            => Delete(employee);

        public IQueryable<Employee> GetAllEmployees(bool trackchanges)
            => FindAll(trackchanges).OrderBy(e => e.Id);

        public async Task<Employee> GetEmployeeByIdAsync(int id, bool trackchanges)
            => await FindByCondition(e => e.Id.Equals(id), trackchanges).SingleOrDefaultAsync();

        public async Task<List<int>> GetSubordinatesAsync(int id, bool trackchanges)
            => await FindByCondition(e => e.ManagerId.Equals(id), trackchanges).Select(e => e.Id).ToListAsync();

        public void UpdateEmployee(Employee employee)
            => Update(employee);
    }
}
