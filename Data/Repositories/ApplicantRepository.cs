
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
