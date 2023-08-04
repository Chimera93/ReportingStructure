using System;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public string CompensationId { get; set; }
        public Employee employee { get; set; }
        public double salary { get; set; }
        public DateTime effectiveDate { get; set; }
    }
}
