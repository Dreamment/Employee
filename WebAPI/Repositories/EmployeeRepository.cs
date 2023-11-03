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

        public async Task<bool> CheckEmployeeByRegistrationNumber(string registrationNumber, bool trackchanges)
        {
            var employee = await FindByCondition(e => e.RegistrationNumber.Equals(registrationNumber), trackchanges).SingleOrDefaultAsync();
            if (employee == null)
                return false;
            return true;
        }

        public int CreateEmployee(Employee employee)
        {
            Create(employee);
            return employee.Id;
        }

        public void DeleteEmployee(Employee employee)
            => Delete(employee);

        public IQueryable<Employee> GetAllEmployees(bool trackchanges)
            => FindAll(trackchanges).OrderBy(e => e.Id);

        public async Task<Employee> GetEmployeeById(int id, bool trackchanges)
            => await FindByCondition(e => e.Id.Equals(id), trackchanges).SingleOrDefaultAsync();

        public async Task<List<int>> GetSubordinates(int id, bool trackchanges)
            => await FindByCondition(e => e.ManagerId.Equals(id), trackchanges).Select(e => e.Id).ToListAsync();

        public void UpdateEmployee(Employee employee)
            => Update(employee);
    }
}
