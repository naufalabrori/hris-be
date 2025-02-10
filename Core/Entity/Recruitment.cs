using HRIS.Core.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class Recruitment : BaseEntity
    {
        public Recruitment() { }

        public Recruitment(RecruitmentDto recruitment)
        {
            JobTitleId = Guid.Parse(recruitment.jobTitleId);
            DepartmentId = Guid.Parse(recruitment.departmentId);
            PostingDate = recruitment.postingDate;
            ClosingDate = recruitment.closingDate;
            Status = recruitment.status;
        }

        public Guid Id { get; set; } = default!;
        public Guid JobTitleId { get; set; } = default!;
        public Guid DepartmentId { get; set; } = default!;
        public DateTime PostingDate { get; set; }
        public DateTime ClosingDate { get; set; }
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;

        public void UpdateRecruitment(RecruitmentDto recruitment)
        {
            JobTitleId = Guid.TryParse(recruitment.jobTitleId, out var jobTitleId) ? jobTitleId : JobTitleId;
            DepartmentId = Guid.TryParse(recruitment.departmentId, out var departmentId) ? departmentId : DepartmentId;
            PostingDate = recruitment?.postingDate ?? PostingDate;
            ClosingDate = recruitment?.closingDate ?? ClosingDate;
            Status = recruitment?.status ?? Status;
        }
    }
}
