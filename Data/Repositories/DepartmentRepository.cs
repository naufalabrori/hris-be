
namespace HRIS.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HrisContext _hrisContext;

        public DepartmentRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Department department, CancellationToken cancellationToken)
        {
            _hrisContext.Departments.Add(department);
        }

        public async Task<Department?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid departmentId = Guid.Parse(id);
            var department = await _hrisContext.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == departmentId, cancellationToken);
            return department;
        }

        public async Task<Department?> GetByDepartmentNameAsync(string departmentName, CancellationToken cancellationToken)
        {
            var department = await _hrisContext.Departments.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.DepartmentName == departmentName, cancellationToken);
            return department;
        }

        public async Task<DepartmentsResponseDto> GetAllAsync(DepartmentQueryDto departmentQueryDto, CancellationToken cancellationToken)
        {
            var query = from dep in _hrisContext.Departments
                        join emp in _hrisContext.Employees on dep.ManagerId equals emp.Id into empGroup
                        from emp in empGroup.DefaultIfEmpty() // Left join
                        select new DepartmenExtDto
                        {
                            Id = dep.Id,
                            DepartmentName = dep.DepartmentName,
                            ManagerId = dep.ManagerId,
                            ManagerName = emp != null ? emp.FirstName + " " + emp.LastName : null, // Handle null
                            Location = dep.Location,
                            IsActive = dep.IsActive,
                            CreatedBy = dep.CreatedBy,
                            CreatedByName = dep.CreatedByName,
                            CreatedDate = dep.CreatedDate,
                            ModifiedBy = dep.ModifiedBy,
                            ModifiedByName = dep.ModifiedByName,
                            ModifiedDate = dep.ModifiedDate,
                        };

            if (!string.IsNullOrWhiteSpace(departmentQueryDto.departmentName))
            {
                query = query.Where(x => x.DepartmentName.ToLower().Contains(departmentQueryDto.departmentName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(departmentQueryDto.managerId))
            {
                query = query.Where(x => x.ManagerId.ToString() == departmentQueryDto.managerId);
            }
            if (!string.IsNullOrWhiteSpace(departmentQueryDto?.managerName))
            {
                query = query.Where(x => x.ManagerName.ToLower().Contains(departmentQueryDto.managerName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(departmentQueryDto.location))
            {
                query = query.Where(x => x.Location.ToLower().Contains(departmentQueryDto.location.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(departmentQueryDto?.sortBy) && departmentQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{departmentQueryDto.sortBy} {(departmentQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(departmentQueryDto.offset)
                .Take(departmentQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new DepartmentsResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Department department, CancellationToken cancellationToken)
        {
            _hrisContext.Departments.Update(department);
        }

        public async Task DeleteAsync(Department department, CancellationToken cancellationToken)
        {
            _hrisContext.Departments.Remove(department);
        }
    }
}
