using Microsoft.AspNetCore.JsonPatch;
using WebAPI.DataTransferObjects;
using WebAPI.Entities;

namespace WebAPI.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDtoForGet>> GetAllEmployeesAsync(bool trackChanges);
        Task<EmployeeDtoForGet> GetEmployeeByIdAsync(int id, bool trackChanges);
        Task<int?> CreateEmployeeAsync(EmployeeDtoForCreate employeeDto);
        Task<bool> UpdateEmployeeAsync(int id, EmployeeDtoForUpdate employeeDto, bool trackChanges);
        Task<bool> DeleteEmployeeAsync(int id, bool trackChanges);
        Task PartiallyUpdateEmployeeAsync(EmployeeDtoForGet employeeToUpdateDtoGet, JsonPatchDocument<EmployeeDtoForUpdate> employeePatch);
        Task<List<int>> GetSubordinatesAsync(int id, bool trackChanges);
        Task<bool> CheckEmployeeByRegistrationNumberAsync(string registrationNumber, bool trackChanges);
    }
}
