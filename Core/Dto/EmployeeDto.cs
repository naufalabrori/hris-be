using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Core.Dto
{
    public record EmployeeDto(string firstName, string lastName, string gender, DateTime? dateOfBirth, string email, string phoneNumber, string address, DateTime? hireDate, string jobTitleId,
        string departmentId, string managerId, string employmentStatus, decimal salary);
}
