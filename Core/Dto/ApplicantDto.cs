using System;

namespace HRIS.Core.Dto
{
    public record ApplicantDto(string firstName, string lastName, string email, string phoneNumber, string resume, DateTime applicationDate, string recruitmentId);
}
