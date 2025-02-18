
namespace HRIS.Core.Dto
{
    public record ApplicantDto(string firstName, string lastName, string email, string phoneNumber, string resume, DateTime applicationDate, string recruitmentId);

    public record ApplicantQueryDto(string firstName, string lastName, string email, string phoneNumber, string resume, DateTime applicationDate, string recruitmentId, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record ApplicantsResponseDto(List<Applicant> Data, int TotalData);
}
