
namespace HRIS.Data.Repositories
{
    public class RecruitmentRepository : IRecruitmentRepository
    {
        private readonly HrisContext _hrisContext;

        public RecruitmentRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Recruitment recruitment, CancellationToken cancellationToken)
        {
            _hrisContext.Recruitments.Add(recruitment);
        }

        public async Task<Recruitment?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid recruitmentId = Guid.Parse(id);
            var reacruitment = await _hrisContext.Recruitments.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == recruitmentId, cancellationToken);
            return reacruitment;
        }

        public async Task<RecruitmentsResponseDto> GetAllAsync(RecruitmentQueryDto recruitmentQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.Recruitments.IsActiveRows().AsNoTracking().Select(x => x);

            if (!string.IsNullOrWhiteSpace(recruitmentQueryDto.jobTitleId))
            {
                query = query.Where(x => x.JobTitleId.ToString() == recruitmentQueryDto.jobTitleId);
            }
            if (!string.IsNullOrWhiteSpace(recruitmentQueryDto.departmentId))
            {
                query = query.Where(x => x.DepartmentId.ToString() == recruitmentQueryDto.departmentId);
            }
            if (recruitmentQueryDto?.postingDate != null && recruitmentQueryDto.postingDate != DateTime.MinValue)
            {
                query = query.Where(x => x.PostingDate.Date == recruitmentQueryDto.postingDate.Date);
            } 
            if (recruitmentQueryDto?.closingDate != null && recruitmentQueryDto?.closingDate != DateTime.MinValue)
            {
                query = query.Where(x => x.ClosingDate.Date == recruitmentQueryDto.closingDate.Date);
            }
            if (!string.IsNullOrWhiteSpace(recruitmentQueryDto.status))
            {
                query = query.Where(x => x.Status.ToLower().Contains(recruitmentQueryDto.status.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(recruitmentQueryDto?.sortBy) && recruitmentQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{recruitmentQueryDto.sortBy} {(recruitmentQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(recruitmentQueryDto.offset)
                .Take(recruitmentQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new RecruitmentsResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Recruitment recruitment, CancellationToken cancellationToken)
        {
            _hrisContext.Recruitments.Update(recruitment);
        }

        public async Task DeleteAsync(Recruitment recruitment, CancellationToken cancellationToken)
        {
            _hrisContext.Recruitments.Remove(recruitment);
        }
    }
}
