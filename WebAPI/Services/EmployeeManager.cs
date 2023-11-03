using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System.Diagnostics.Eventing.Reader;
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

        public bool CreateEmployee(Employee employee)
        {
            if (employee == null)
                return false;

            _repositoryManager.Employee.CreateEmployee(employee);
            _repositoryManager.Save();
            return true;
        }

        public bool DeleteEmployee(int id, bool trackChanges)
        {
            var employeeToDelete = GetEmployeeById(id, trackChanges);
            if (employeeToDelete == null)
                return false;
            _repositoryManager.Employee.DeleteEmployee(employeeToDelete);
            _repositoryManager.Save();
            return true;

        }

        public IEnumerable<Employee> GetAllEmployees(bool trackChanges)
            => _repositoryManager.Employee.GetAllEmployees(trackChanges);

        public Employee GetEmployeeById(int id, bool trackChanges)
            => _repositoryManager.Employee.GetEmployeeById(id, trackChanges);

        public void PartiallyUpdateEmployee(Employee employeeToUpdate, JsonPatchDocument<EmployeeDtoForUpdate> employeePatch)
        {
            var employeeDto = _mapper.Map<EmployeeDtoForUpdate>(employeeToUpdate);
            employeePatch.ApplyTo(employeeDto);
            _mapper.Map(employeeDto, employeeToUpdate);
            _repositoryManager.Employee.UpdateEmployee(employeeToUpdate);
            _repositoryManager.Save();
        }

        public bool UpdateEmployee(int id, EmployeeDtoForUpdate employeeDto, bool trackChanges)
        {
            var employeeToUpdate = GetEmployeeById(id, trackChanges);
            if (employeeToUpdate == null)
                return false;
            employeeDto.Id = id;
            employeeToUpdate = _mapper.Map<Employee>(employeeDto);
            _repositoryManager.Employee.UpdateEmployee(employeeToUpdate);
            _repositoryManager.Save();
            return true;
        }
    }
}
