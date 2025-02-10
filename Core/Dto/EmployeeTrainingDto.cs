using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Core.Dto
{
    public record EmployeeTrainingDto(string employeeId, string trainingId, string status);
}
