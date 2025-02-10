using HRIS.Core.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Core.Entity
{
    public class Benefits : BaseEntity
    {
        public Benefits() { }

        public Benefits(BenefitsDto benefits)
        {
            EmployeeId = Guid.Parse(benefits.employeeId);
            BenefitType = benefits.benefitType;
            StartDate = benefits.startDate;
            EndDate = benefits.endDate;
            Details = benefits.details;
        }

        public Guid Id { get; set; } = default!;
        public Guid EmployeeId { get; set; } = default!;
        [StringLength(100)]
        public string BenefitType { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Details { get; set; } = default!;

        public void UpdateBenefits(BenefitsDto benefits)
        {
            EmployeeId = Guid.TryParse(benefits?.employeeId, out var employeeId) ? employeeId : EmployeeId;
            BenefitType = benefits.benefitType ?? BenefitType;
            StartDate = benefits?.startDate ?? StartDate;
            EndDate = benefits?.endDate ?? EndDate;
            Details = benefits?.details ?? Details;
        }
    }
}
