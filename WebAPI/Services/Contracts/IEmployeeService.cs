﻿using Microsoft.AspNetCore.JsonPatch;
using WebAPI.DataTransferObjects;
using WebAPI.Entities;

namespace WebAPI.Services.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDtoForGet> GetAllEmployees(bool trackChanges);
        EmployeeDtoForGet GetEmployeeById(int id, bool trackChanges);
        int? CreateEmployee(EmployeeDtoForCreate employeeDto);
        bool UpdateEmployee(int id, EmployeeDtoForUpdate employeeDto, bool trackChanges);
        bool DeleteEmployee(int id, bool trackChanges);
        void PartiallyUpdateEmployee(EmployeeDtoForGet employeeToUpdateDtoGet, JsonPatchDocument<EmployeeDtoForUpdate> employeePatch);
        List<int> GetSuborditanes(int id, bool trackChanges);
        bool CheckEmployeeByRegistrationNumber(string registrationNumber, bool trackChanges);
    }
}
