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
            var employee = (await FindByConditionAsync(e => e.RegistrationNumber.Equals(registrationNumber), trackchanges)).SingleOrDefault();
            if (employee == null)
                return false;
            return true;
        }

        public async Task CreateEmployeeAsync(Employee employee) 
            =>await CreateAsync(employee);

        public async Task DeleteEmployeeAsync(Employee employee)
            => await DeleteAsync(employee);

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(bool trackchanges)
        {
            var employees = (await FindAllAsync(trackchanges)).OrderBy(e => e.Id).ToList();
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id, bool trackchanges)
        {
            var entity = await FindByConditionAsync(e => e.Id.Equals(id), trackchanges);
            return entity.SingleOrDefault();
        }

        public async Task<List<int>> GetSubordinatesAsync(int id, bool trackchanges)
        {
            var entity = await FindByConditionAsync(e => e.ManagerId.Equals(id), trackchanges);
            return entity.Select(e => e.Id).ToList();
        }
        public async Task UpdateEmployeeAsync(Employee employee)
            => await UpdateAsync(employee);
    }
}
