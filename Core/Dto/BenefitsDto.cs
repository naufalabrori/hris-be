using System;

namespace HRIS.Core.Dto
{
    public record BenefitsDto(string employeeId, string benefitType, DateTime startDate, DateTime endDate, string details);
}
