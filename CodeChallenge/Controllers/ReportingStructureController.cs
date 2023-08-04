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
    [Route("api/[controller]")]
    [ApiController]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("{id}", Name = "GetEmployeeReportingStructure")]
        public IActionResult GetEmployeeReportingStructure(String id)
        {
            _logger.LogDebug($"Received employee reporting structure get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var reportingStructure = new ReportingStructure();
            reportingStructure.employee = employee;
            reportingStructure.numberOfReports = _employeeService.GetNumberOfReports(employee);
                
            return Ok(reportingStructure);
        }
    }
}
