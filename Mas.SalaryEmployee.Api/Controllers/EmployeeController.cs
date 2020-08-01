using System;
using System.Linq;
using System.Threading.Tasks;
using Mas.SalaryEmployee.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mas.SalaryEmployee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var employees = await this._employeeService.GetAsync();

            if (employees.Any())
                return Ok(employees);

            return NoContent();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest($"Invalid id '{id}'");

            var employee = await this._employeeService.GetAsync(id);

            if (employee is null)
                return NoContent();

            return Ok(employee);
        }
    }
}
