﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataTransferObjects;
using WebAPI.Entities;
using WebAPI.Services.Contracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet(Name = "GetAllEmployeesAsync")]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var employees = await _serviceManager.Employee.GetAllEmployeesAsync(false);
            if (employees == null)
                return NotFound();
            return Ok(employees);
        }

        [HttpGet("{id:int}", Name = "GetEmployeeByIdAsync")]
        public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute(Name = "id")] int id)
        {
            var employee = await _serviceManager.Employee.GetEmployeeByIdAsync(id, false);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost(Name = "CreateEmployeeAsync")]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeeDtoForCreate employeeDto)
        {
            if (employeeDto == null)
                return BadRequest("Employee object is null");
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");
            var Id = await _serviceManager.Employee.CreateEmployeeAsync(employeeDto);
            if (Id == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Employee could not be saved");
            return CreatedAtRoute("GetEmployeeById", new { id = Id }, employeeDto);
        }

        [HttpPut("{id:int}", Name = "UpdateEmployeeAsync")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromRoute(Name = "id")] int id, [FromBody] EmployeeDtoForUpdate employeeDto)
        {
            if (employeeDto == null)
                return BadRequest("Employee object is null");
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");
            if (await _serviceManager.Employee.CheckEmployeeByRegistrationNumberAsync(employeeDto.RegistrationNumber, false))
            {
                return BadRequest("This Registration Number is being used by another employee.");
            }
            if (employeeDto.ManagerId != null)
            {
                var entity = await _serviceManager.Employee.GetEmployeeByIdAsync((int)employeeDto.ManagerId, false);
                if (entity == null)
                    return NotFound($"The manager with ID {employeeDto.ManagerId} could not be found.");
                if (entity.ManagerId == id)
                    return BadRequest($"You cannot assign the employee with ID {employeeDto.ManagerId}" +
                        $" as the manager to the employee with ID {id}" +
                        $" because the employee with ID {id}" +
                        $" is the manager of employee with ID {employeeDto.ManagerId}.");
            }
            bool control = await _serviceManager.Employee.UpdateEmployeeAsync(id, employeeDto, false);
            if (control)
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id:int}", Name = "DeleteEmployeeAsync")]
        public async Task<IActionResult> DeleteEmployeeAsync([FromRoute(Name = "id")] int id)
        {
            var employee = await _serviceManager.Employee.GetEmployeeByIdAsync(id, false);
            if (employee == null)
                return NotFound();
            await _serviceManager.Employee.DeleteEmployeeAsync(id, false);
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "PartiallyUpdateEmployeeAsync")]
        public async Task<IActionResult> PartiallyUpdateEmployeeAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<EmployeeDtoForUpdate> employeePatch)
        {
            if (employeePatch == null)
                return BadRequest("Employee object is null");
            var employeeToUpdateDto = await _serviceManager.Employee.GetEmployeeByIdAsync(id, false);
            if (employeeToUpdateDto == null)
                return NotFound();

            if (employeePatch.Operations.Any(op => op.path == "managerId"))
            {
                int newManagerId = Convert.ToInt32(employeePatch.Operations.FirstOrDefault(op => op.path == "managerId").value);
                var entity = await _serviceManager.Employee.GetEmployeeByIdAsync(newManagerId, false);
                if (entity == null)
                    return NotFound($"Manager with ID {newManagerId} could not be found.");
                if (entity.ManagerId == id)
                    return BadRequest($"You cannot assign the employee with ID {newManagerId}" +
                        $" as the manager to the employee with ID {id}" +
                        $" because the employee with ID {id}" +
                        $" is the manager of employee with ID {newManagerId}.");
            }
            if (employeePatch.Operations.Any(op => op.path == "registrationNumber"))
            {
                string newRegistrationNumber = employeePatch.Operations.FirstOrDefault(op => op.path == "registrationNumber").value.ToString();
                if (await _serviceManager.Employee.CheckEmployeeByRegistrationNumberAsync(newRegistrationNumber, false))
                {
                    return BadRequest("This Registration Number is being used by another employee.");
                }
            }
            await _serviceManager.Employee.PartiallyUpdateEmployeeAsync(employeeToUpdateDto, employeePatch);

            return NoContent();
        }
    }
}
