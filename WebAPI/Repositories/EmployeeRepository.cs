using WebAPI.Entities;

namespace WebAPI.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base (context)
        {
            
        }
        public void CreateEmployee(Employee employee) 
            => Create(employee);

        public void DeleteEmployee(Employee employee) 
            => Delete(employee);

        public IQueryable<Employee> GetAllEmployees(bool trackchanges) 
            => FindAll(trackchanges).OrderBy(e => e.Id);

        public Employee GetEmployeeById(int id, bool trackchanges) 
            => FindByCondition(e => e.Id.Equals(id), trackchanges).SingleOrDefault();

        public void UpdateEmployee(Employee employee) 
            => Update(employee);
    }
}
