using Microsoft.AspNetCore.JsonPatch;
using WebAPI.DataTransferObjects;
using WebAPI.Entities;

namespace WebAPI.Services.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployees(bool trackChanges);
        Employee GetEmployeeById(int id, bool trackChanges);
        int? CreateEmployee(EmployeeDtoForCreate employeeDto);
        bool UpdateEmployee(int id, EmployeeDtoForUpdate employeeDto, bool trackChanges);
        bool DeleteEmployee(int id, bool trackChanges);
        void PartiallyUpdateEmployee(Employee employeeToUpdate, JsonPatchDocument<EmployeeDtoForUpdate> employeePatch);
    }
}
