using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using System.Xml.Linq;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository, ICompensationRepository compensationRepository)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        public int GetNumberOfReports(Employee employee)
        {
            var totalReports = 0;

            var nodes = new Stack<Employee>();
            nodes.Push(employee);

            while (nodes.Any())
            {
                Employee node = GetById(nodes.Pop().EmployeeId);

                if (node.DirectReports != null)
                {
                    totalReports += node.DirectReports.Count;

                    foreach (var n in node.DirectReports)
                    {
                        nodes.Push(n);
                    }
                }
            }

            return totalReports;
        }

        public Compensation CreateEmployeeCompensation(Compensation comp)
        {
            comp.employee = GetById(comp.employee.EmployeeId);
            return _compensationRepository.Add(comp);
        }

        public Compensation GetCompensationByEmployeeId(string id)
        {
            return _compensationRepository.GetByEmployeeId(id);
        }
    }
}
