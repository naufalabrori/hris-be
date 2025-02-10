using System;

namespace HRIS.Core.Dto
{
    public record RecruitmentDto(string jobTitleId, string departmentId, DateTime postingDate, DateTime closingDate, string status);
}
