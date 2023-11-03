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

        [HttpGet(Name = "GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            var employees = _serviceManager.Employee.GetAllEmployees(false);
            if (employees == null)
                return NotFound();
            return Ok(employees);
        }

        [HttpGet("{id:int}", Name = "GetEmployeeById")]
        public IActionResult GetEmployeeById([FromRoute(Name = "id")] int id)
        {
            var employee = _serviceManager.Employee.GetEmployeeById(id, false);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost(Name = "CreateEmployee")]
        public IActionResult CreateEmployee([FromBody] EmployeeDtoForCreate employeeDto)
        {
            if (employeeDto == null)
                return BadRequest("Employee object is null");
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");
            var Id = _serviceManager.Employee.CreateEmployee(employeeDto);
            if (Id == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Employee could not be saved");
            return CreatedAtRoute("GetEmployeeById", new { id = Id }, employeeDto);
        }

        [HttpPut("{id:int}", Name = "UpdateEmployee")]
        public IActionResult UpdateEmployee([FromRoute(Name = "id")] int id, [FromBody] EmployeeDtoForUpdate employeeDto)
        {
            if (employeeDto == null)
                return BadRequest("Employee object is null");
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");
            if (_serviceManager.Employee.CheckEmployeeByRegistrationNumber(employeeDto.RegistrationNumber, false))
            {
                return BadRequest("This Registration Number is being used by another employee.");
            }
            if (employeeDto.ManagerId != null)
            {
                var entity = _serviceManager.Employee.GetEmployeeById((int)employeeDto.ManagerId, false);
                if (entity == null)
                    return NotFound($"The manager with ID {employeeDto.ManagerId} could not be found.");
                if (entity.ManagerId == id)
                    return BadRequest($"You cannot assign the employee with ID {employeeDto.ManagerId}" +
                        $" as the manager to the employee with ID {id}" +
                        $" because the employee with ID {id}" +
                        $" is the manager of employee with ID {employeeDto.ManagerId}.");
            }
            bool control = _serviceManager.Employee.UpdateEmployee(id, employeeDto, false);
            if (control)
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id:int}", Name = "DeleteEmployee")]
        public IActionResult DeleteEmployee([FromRoute(Name = "id")] int id)
        {
            var employee = _serviceManager.Employee.GetEmployeeById(id, false);
            if (employee == null)
                return NotFound();
            _serviceManager.Employee.DeleteEmployee(id, false);
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "PartiallyUpdateEmployee")]
        public IActionResult PartiallyUpdateEmployee([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<EmployeeDtoForUpdate> employeePatch)
        {
            if (employeePatch == null)
                return BadRequest("Employee object is null");
            var employeeToUpdateDto = _serviceManager.Employee.GetEmployeeById(id, false);
            if (employeeToUpdateDto == null)
                return NotFound();

            if (employeePatch.Operations.Any(op => op.path == "managerId"))
            {
                int newManagerId = Convert.ToInt32(employeePatch.Operations.FirstOrDefault(op => op.path == "managerId").value);
                var entity = _serviceManager.Employee.GetEmployeeById(newManagerId, false);
                if (entity == null)
                    return NotFound($"Manager with ID {newManagerId} could not be found.");
                if (entity.ManagerId == id)
                    return BadRequest($"You cannot assign the employee with ID {newManagerId}" +
                        $" as the manager to the employee with ID {id}" +
                        $" because the employee with ID {id}" +
                        $" is the manager of employee with ID {newManagerId}.");
            }
            if(employeePatch.Operations.Any(op => op.path == "registrationNumber"))
            {
                string newRegistrationNumber = employeePatch.Operations.FirstOrDefault(op => op.path == "registrationNumber").value.ToString();
                if (_serviceManager.Employee.CheckEmployeeByRegistrationNumber(newRegistrationNumber, false))
                {
                    return BadRequest("This Registration Number is being used by another employee.");
                }
            }
            _serviceManager.Employee.PartiallyUpdateEmployee(employeeToUpdateDto, employeePatch);

            return NoContent();
        }
    }
}
