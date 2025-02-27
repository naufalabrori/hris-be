
namespace HRIS.Data.Repositories
{
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly HrisContext _hrisContext;

        public JobTitleRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(JobTitle jobTitle, CancellationToken cancellationToken)
        {
            _hrisContext.JobTitles.Add(jobTitle);
        }

        public async Task<JobTitle?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid jobTitleId = Guid.Parse(id);
            var jobTitle = await _hrisContext.JobTitles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == jobTitleId, cancellationToken);
            return jobTitle;
        }

        public async Task<JobTitlesResponseDto> GetAllAsync(JobTitleQueryDto jobTitleQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.JobTitles.Select(x => x);

            if (!string.IsNullOrWhiteSpace(jobTitleQueryDto.title))
            {
                query = query.Where(x => x.Title.Contains(jobTitleQueryDto.title));
            }
            if (!string.IsNullOrWhiteSpace(jobTitleQueryDto.description))
            {
                query = query.Where(x => x.Description.Contains(jobTitleQueryDto.description));
            }
            if (jobTitleQueryDto?.minSalary != null)
            {
                query = query.Where(x => x.MinSalary == jobTitleQueryDto.minSalary);
            }
            if (jobTitleQueryDto?.maxSalary != null)
            {
                query = query.Where(x => x.MaxSalary == jobTitleQueryDto.maxSalary);
            }
            if (!string.IsNullOrWhiteSpace(jobTitleQueryDto?.sortBy) && jobTitleQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{jobTitleQueryDto.sortBy} {(jobTitleQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }
            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
            .Skip(jobTitleQueryDto.offset)
            .Take(jobTitleQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new JobTitlesResponseDto(page, totalData);
        }

        public async Task UpdateAsync(JobTitle jobTitle, CancellationToken cancellationToken)
        {
            _hrisContext.JobTitles.Update(jobTitle);
        }

        public async Task DeleteAsync(JobTitle jobTitle, CancellationToken cancellationToken)
        {
            _hrisContext.JobTitles.Remove(jobTitle);
        }
    }
}
