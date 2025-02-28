
using HRIS.Core.Dto;

namespace HRIS.Data.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly HrisContext _hrisContext;

        public ApplicantRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Applicant applicant, CancellationToken cancellationToken)
        {
            _hrisContext.Applicants.Add(applicant);
        }

        public async Task<Applicant?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var applicantId = Guid.Parse(id);
            var applicant = await _hrisContext.Applicants.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == applicantId, cancellationToken);
            return applicant;
        }

        public async Task<ApplicantsResponseDto> GetAllAsync(ApplicantQueryDto applicantQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.Applicants.IsActiveRows().AsNoTracking().Select(x => x);

            if (!string.IsNullOrWhiteSpace(applicantQueryDto?.firstName))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(applicantQueryDto.firstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(applicantQueryDto?.lastName))
            {
                query = query.Where(x => x.LastName.ToLower().Contains(applicantQueryDto.lastName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(applicantQueryDto?.email))
            {
                query = query.Where(x => x.Email.ToLower().Contains(applicantQueryDto.email.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(applicantQueryDto?.phoneNumber))
            {
                query = query.Where(x => x.PhoneNumber.Contains(applicantQueryDto.phoneNumber));
            }
            if (applicantQueryDto?.applicationDate != null && applicantQueryDto.applicationDate != DateTime.MinValue)
            {
                query = query.Where(x => x.ApplicationDate.Date == applicantQueryDto.applicationDate.Date);
            } 
            if (!string.IsNullOrWhiteSpace(applicantQueryDto?.recruitmentId))
            {
                query = query.Where(x => x.RecruitmentId.ToString() == applicantQueryDto.recruitmentId);
            }
            if (!string.IsNullOrWhiteSpace(applicantQueryDto?.sortBy) && applicantQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{applicantQueryDto.sortBy} {(applicantQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(applicantQueryDto.offset)
                .Take(applicantQueryDto.limit)
                .AsNoTracking();

            var page = await pageQuery.ToListAsync(cancellationToken);

            return new ApplicantsResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Applicant applicant, CancellationToken cancellationToken)
        {
            _hrisContext.Applicants.Update(applicant);
        }

        public async Task DeleteAsync(Applicant applicant, CancellationToken cancellationToken)
        {
            _hrisContext.Applicants.Remove(applicant);
        }
    }
}
