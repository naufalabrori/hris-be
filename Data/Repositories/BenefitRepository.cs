
namespace HRIS.Data.Repositories
{
    public class BenefitRepository : IBenefitRepository
    {
        private readonly HrisContext _hrisContext;

        public BenefitRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Benefits benefit, CancellationToken cancellationToken)
        {
            _hrisContext.Benefits.Add(benefit);
        }

        public async Task<Benefits?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var benefitId = Guid.Parse(id);
            var benefit = await _hrisContext.Benefits.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == benefitId, cancellationToken);
            return benefit;
        }

        public async Task<List<Benefits>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken)
        {
            var benefits = await _hrisContext.Benefits.IsActiveRows().AsNoTracking().Where(x => x.EmployeeId == Guid.Parse(employeeId)).ToListAsync();
            return benefits;
        }

        public async Task<BenefitsResponseDto> GetAllAsync(BenefitQueryDto benefitQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.Benefits.IsActiveRows().AsNoTracking().Select(x => x);

            if (!string.IsNullOrWhiteSpace(benefitQueryDto.employeeId))
            {
                query = query.Where(x => x.EmployeeId.ToString() == benefitQueryDto.employeeId);
            }
            if (!string.IsNullOrWhiteSpace(benefitQueryDto.benefitType))
            {
                query = query.Where(x => x.BenefitType.ToLower().Contains(benefitQueryDto.benefitType.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(benefitQueryDto.details))
            {
                query = query.Where(x => x.Details.ToLower().Contains(benefitQueryDto.details.ToLower()));
            }
            if (benefitQueryDto?.startDate != null && benefitQueryDto.startDate != DateTime.MinValue)
            {
                query = query.Where(x => x.StartDate.Date >= benefitQueryDto.startDate.Date);
            }
            if (benefitQueryDto?.endDate != null && benefitQueryDto.endDate != DateTime.MinValue)
            {
                query = query.Where(x => x.EndDate.Date <= benefitQueryDto.endDate.Date);
            }
            if (!string.IsNullOrWhiteSpace(benefitQueryDto?.sortBy) && benefitQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{benefitQueryDto.sortBy} {(benefitQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(benefitQueryDto.offset)
                .Take(benefitQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new BenefitsResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Benefits benefit, CancellationToken cancellationToken)
        {
            _hrisContext.Benefits.Update(benefit);
        }

        public async Task DeleteAsync(Benefits benefit, CancellationToken cancellationToken)
        {
            _hrisContext.Benefits.Remove(benefit);
        }
    }
}
