using Microsoft.AspNetCore.Http;
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
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest("Employee object is null");
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");
            _serviceManager.Employee.CreateEmployee(employee);
            return CreatedAtRoute("GetEmployeeById", new { id = employee.Id }, employee);
        }

        [HttpPut("{id:int}", Name = "UpdateEmployee")]
        public IActionResult UpdateEmployee([FromRoute(Name = "id")] int id, [FromBody] EmployeeDtoForUpdate employeeDto)
        {
            if (employeeDto == null)
                return BadRequest("Employee object is null");
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");
            _serviceManager.Employee.UpdateEmployee(id, employeeDto, false);
            return NoContent();
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
            var employeeToUpdate = _serviceManager.Employee.GetEmployeeById(id, true);
            if (employeeToUpdate == null)
                return NotFound();

            _serviceManager.Employee.PartiallyUpdateEmployee(employeeToUpdate, employeePatch);

            return NoContent();
        }
    }
}
