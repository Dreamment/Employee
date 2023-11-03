using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using WebAPI.DataTransferObjects;
using WebAPI.Entities;
using WebAPI.Repositories.Contracts;
using WebAPI.Services.Contracts;

namespace WebAPI.Services
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EmployeeManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<bool> CheckEmployeeByRegistrationNumberAsync(string registrationNumber, bool trackChanges)
            => await _repositoryManager.Employee.CheckEmployeeByRegistrationNumberAsync(registrationNumber, trackChanges);

        public async Task<int?> CreateEmployeeAsync(EmployeeDtoForCreate employeeDto)
        {
            if (employeeDto == null)
                return null;

            var id = await _repositoryManager.Employee.CreateEmployeeAsync(_mapper.Map<Employee>(employeeDto));
            await _repositoryManager.SaveAsync();
            return id;
        }

        public async Task<bool> DeleteEmployeeAsync(int id, bool trackChanges)
        {
            var employeeToDeleteDto = GetEmployeeByIdAsync(id, trackChanges);
            var employeeToDelete = _mapper.Map<Employee>(employeeToDeleteDto);
            if (employeeToDelete == null)
                return false;
            _repositoryManager.Employee.DeleteEmployee(employeeToDelete);
            await _repositoryManager.SaveAsync();
            return true;

        }

        public async Task<IEnumerable<EmployeeDtoForGet>> GetAllEmployeesAsync(bool trackChanges)
        {
            var employees = _repositoryManager.Employee.GetAllEmployees(trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDtoForGet>>(employees);
            foreach (var employee in employeesDto)
            {
                var subordinates = await _repositoryManager.Employee.GetSubordinatesAsync(employee.Id, trackChanges);
                employee.SubordinatesIds = subordinates;
            }
            return employeesDto;
        }

        public async Task<EmployeeDtoForGet> GetEmployeeByIdAsync(int id, bool trackChanges)
        {
            var employee = await _repositoryManager.Employee.GetEmployeeByIdAsync(id, trackChanges);
            if (employee == null)
                return null;
            var employeeDto = _mapper.Map<EmployeeDtoForGet>(employee);
            var subordinates = await GetSubordinatesAsync(employee.Id, trackChanges);
            employeeDto.SubordinatesIds = subordinates;
            return employeeDto;
        }

        public async Task<List<int>> GetSubordinatesAsync(int id, bool trackChanges)
            => await _repositoryManager.Employee.GetSubordinatesAsync(id, trackChanges);

        public async Task PartiallyUpdateEmployeeAsync(EmployeeDtoForGet employeeToUpdateDtoGet, JsonPatchDocument<EmployeeDtoForUpdate> employeePatch)
        {
            var employeeDto = _mapper.Map<EmployeeDtoForUpdate>(employeeToUpdateDtoGet);
            employeePatch.ApplyTo(employeeDto);
            var employeeToUpdate = _mapper.Map<Employee>(employeeDto);
            _repositoryManager.Employee.UpdateEmployee(employeeToUpdate);
            await _repositoryManager.SaveAsync();
        }

        public async Task<bool> UpdateEmployeeAsync(int id, EmployeeDtoForUpdate employeeDto, bool trackChanges)
        {
            var employeeToUpdateDto = await GetEmployeeByIdAsync(id, trackChanges);
            if (employeeToUpdateDto == null)
                return false;
            employeeDto.Id = id;
            var employeeToUpdate = _mapper.Map<Employee>(employeeDto);
            //employee gönder
            _repositoryManager.Employee.UpdateEmployee(employeeToUpdate);
            await _repositoryManager.SaveAsync();
            return true;
        }
    }
}
