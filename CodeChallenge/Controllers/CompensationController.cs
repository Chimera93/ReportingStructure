using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{

    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public CompensationController(ILogger<CompensationController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation comp)
        {
            _logger.LogDebug($"Received compensation create request for employee id '{comp.employee.EmployeeId}'");

            _employeeService.CreateEmployeeCompensation(comp);

            return CreatedAtRoute("getCompensationByEmployeeID", new { id = comp.employee.EmployeeId }, comp);
        }

        [HttpGet("{id}", Name = "getCompensationByEmployeeID")]
        public IActionResult GetCompensationByEmployeeID(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var comp = _employeeService.GetCompensationByEmployeeId(id);

            if (comp == null)
                return NotFound();

            return Ok(comp);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody] Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }
    }
}
