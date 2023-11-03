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

        public bool CheckEmployeeByRegistrationNumber(string registrationNumber, bool trackChanges) 
            => _repositoryManager.Employee.CheckEmployeeByRegistrationNumber(registrationNumber, trackChanges);

        public int? CreateEmployee(EmployeeDtoForCreate employeeDto)
        {
            if (employeeDto == null)
                return null;

            var id = _repositoryManager.Employee.CreateEmployee(_mapper.Map<Employee>(employeeDto));
            _repositoryManager.Save();
            return id;
        }

        public bool DeleteEmployee(int id, bool trackChanges)
        {
            var employeeToDeleteDto = GetEmployeeById(id, trackChanges);
            var employeeToDelete = _mapper.Map<Employee>(employeeToDeleteDto);
            if (employeeToDelete == null)
                return false;
            _repositoryManager.Employee.DeleteEmployee(employeeToDelete);
            _repositoryManager.Save();
            return true;

        }

        public IEnumerable<EmployeeDtoForGet> GetAllEmployees(bool trackChanges)
        {
            var employees = _repositoryManager.Employee.GetAllEmployees(trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDtoForGet>>(employees);
            foreach (var employee in employeesDto)
            {
                var subordinates = _repositoryManager.Employee.GetSubordinates(employee.Id, trackChanges);
                employee.SubordinatesIds = subordinates;
            }
            return employeesDto;
        }

        public EmployeeDtoForGet GetEmployeeById(int id, bool trackChanges)
        {
            var employee = _repositoryManager.Employee.GetEmployeeById(id, trackChanges);
            if (employee == null)
                return null;
            var employeeDto = _mapper.Map<EmployeeDtoForGet>(employee);
            var subordinates = _repositoryManager.Employee.GetSubordinates(employee.Id, trackChanges);
            employeeDto.SubordinatesIds = subordinates;
            return employeeDto;
        }

        public List<int> GetSuborditanes(int id, bool trackChanges)
            => _repositoryManager.Employee.GetSubordinates(id, trackChanges);

        public void PartiallyUpdateEmployee(EmployeeDtoForGet employeeToUpdateDtoGet, JsonPatchDocument<EmployeeDtoForUpdate> employeePatch)
        {
            var employeeDto = _mapper.Map<EmployeeDtoForUpdate>(employeeToUpdateDtoGet);
            employeePatch.ApplyTo(employeeDto);
            var employeeToUpdate = _mapper.Map<Employee>(employeeDto);
            _repositoryManager.Employee.UpdateEmployee(employeeToUpdate);
            _repositoryManager.Save();
        }

        public bool UpdateEmployee(int id, EmployeeDtoForUpdate employeeDto, bool trackChanges)
        {
            var employeeToUpdateDto = GetEmployeeById(id, trackChanges);
            if (employeeToUpdateDto == null)
                return false;
            employeeDto.Id = id;
            var employeeToUpdate = _mapper.Map<Employee>(employeeDto);
            //employee gönder
            _repositoryManager.Employee.UpdateEmployee(employeeToUpdate);
            _repositoryManager.Save();
            return true;
        }
    }
}
