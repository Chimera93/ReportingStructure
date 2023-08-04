using CodeChallenge.Models;
using System;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation Add(Compensation compensation);

        Compensation GetByEmployeeId(String id);
    }
}
