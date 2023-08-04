using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {            
            compensation.CompensationId = Guid.NewGuid().ToString();
            _employeeContext.EmployeeCompensation.Add(compensation);
            _employeeContext.SaveChanges();
            return compensation;
        }

        public Compensation GetByEmployeeId(string id)
        {
            return _employeeContext.EmployeeCompensation
                .Include(x => x.employee)
                .Where(x => x.employee.EmployeeId == id)
                .FirstOrDefault()
                ;
        }
    }
}
