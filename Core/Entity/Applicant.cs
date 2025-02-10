using HRIS.Core.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class Applicant : BaseEntity
    {
        public Applicant() { }

        public Applicant(ApplicantDto applicant)
        {
            FirstName = applicant.firstName;
            LastName = applicant.lastName;
            Email = applicant.email;
            PhoneNumber = applicant.phoneNumber;
            Resume = applicant.resume;
            ApplicationDate = applicant.applicationDate;
            RecruitmentId = Guid.Parse(applicant.recruitmentId);
        }

        public Guid Id { get; set; } = default!;
        [StringLength(50)]
        public string FirstName { get; set; } = default!;
        [StringLength(50)]
        public string LastName { get; set; } = default!;
        [StringLength(100)]
        public string Email { get; set; } = default!;
        [StringLength(16)]
        public string PhoneNumber{ get; set; } = default!;
        public string Resume {  get; set; } = default!;
        public DateTime ApplicationDate { get; set; }
        public Guid RecruitmentId { get; set; } = default!;

        public void UpdateApplicant(ApplicantDto applicant)
        {
            FirstName = applicant.firstName ?? FirstName;
            LastName = applicant.lastName ?? LastName;
            Email = applicant.email ?? Email;
            PhoneNumber= applicant.phoneNumber ?? PhoneNumber;
            Resume= applicant.resume ?? Resume;
            ApplicationDate = applicant?.applicationDate ?? ApplicationDate;
            RecruitmentId = Guid.TryParse(applicant?.recruitmentId, out var recruitmentId) ? recruitmentId : RecruitmentId;
        }
    }
}
