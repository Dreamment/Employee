using WebAPI.Entities;
using WebAPI.Repositories.Contracts;

namespace WebAPI.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base (context)
        {

        }

        public bool CheckEmployeeByRegistrationNumber(string registrationNumber, bool trackchanges)
        {
            var employee = FindByCondition(e => e.RegistrationNumber.Equals(registrationNumber), trackchanges).SingleOrDefault();
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

        public Employee GetEmployeeById(int id, bool trackchanges)
            => FindByCondition(e => e.Id.Equals(id), trackchanges).SingleOrDefault();

        public List<int> GetSubordinates(int id, bool trackchanges)
            => FindByCondition(e => e.ManagerId.Equals(id), trackchanges).Select(e => e.Id).ToList();

        public void UpdateEmployee(Employee employee)
            => Update(employee);
    }
}
