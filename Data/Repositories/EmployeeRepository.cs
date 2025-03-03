
namespace HRIS.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrisContext _hrisContext;

        public EmployeeRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
        {
            _hrisContext.Employees.Add(employee);
        }

        public async Task<EmployeeExtDto?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid empId = Guid.Parse(id);
            var employee = await GetEmployeeExtQuery().AsNoTracking().FirstAsync(x => x.Id == empId, cancellationToken);
            return employee;
        }

        public async Task<EmployeesResponseDto> GetAllAsync(EmployeeQueryDto employeeQueryDto, CancellationToken cancellationToken)
        {
            var query = GetEmployeeExtQuery();

            if (!string.IsNullOrWhiteSpace(employeeQueryDto.firstName))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(employeeQueryDto.firstName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(employeeQueryDto.lastName))
            {
                query = query.Where(x => x.LastName.ToLower().Contains(employeeQueryDto.lastName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(employeeQueryDto.gender))
            {
                query = query.Where(x => x.Gender.ToLower().Contains(employeeQueryDto.gender.ToLower()));
            }
            if (employeeQueryDto.dateOfBirth != null && employeeQueryDto.dateOfBirth != DateTime.MinValue)
            {
                query = query.Where(x => x.DateOfBirth.Value.Date == employeeQueryDto.dateOfBirth.Value.Date);
            }
            if (!string.IsNullOrWhiteSpace(employeeQueryDto.email))
            {
                query = query.Where(x => x.Email.ToLower().Contains(employeeQueryDto.email.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(employeeQueryDto.phoneNumber))
            {
                query = query.Where(x => x.PhoneNumber.ToLower().Contains(employeeQueryDto.phoneNumber.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(employeeQueryDto.address))
            {
                query = query.Where(x => x.Address.ToLower().Contains(employeeQueryDto.address.ToLower()));
            }
            if (employeeQueryDto.hireDate != null && employeeQueryDto.hireDate != DateTime.MinValue)
            {
                query = query.Where(x => x.HireDate.Value.Date == employeeQueryDto.hireDate.Value.Date);
            }
            if (!string.IsNullOrWhiteSpace(employeeQueryDto.departmentName))
            {
                query = query.Where(x => x.DepartmentName.ToLower().Contains(employeeQueryDto.departmentName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(employeeQueryDto.jobName))
            {
                query = query.Where(x => x.JobName.ToLower().Contains(employeeQueryDto.jobName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(employeeQueryDto.managerName))
            {
                query = query.Where(x => x.ManagerName.ToLower().Contains(employeeQueryDto.managerName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(employeeQueryDto?.sortBy) && employeeQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{employeeQueryDto.sortBy} {(employeeQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(employeeQueryDto.offset)
                .Take(employeeQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new EmployeesResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
        {
            _hrisContext.Employees.Update(employee);
        }

        public async Task DeleteAsync(Employee employee, CancellationToken cancellationToken)
        {
            _hrisContext.Employees.Remove(employee);
        }

        private IQueryable<EmployeeExtDto> GetEmployeeExtQuery()
        {
            var query = from emp in _hrisContext.Employees
                        join dep in _hrisContext.Departments on emp.DepartmentId equals dep.Id
                        join job in _hrisContext.JobTitles on emp.JobTitleId equals job.Id
                        join mgr in _hrisContext.Employees on emp.ManagerId equals mgr.Id into mgrJoin
                        from mgr in mgrJoin.DefaultIfEmpty() // Left Join untuk manager
                        select new EmployeeExtDto
                        {
                            Id = emp.Id,
                            FirstName = emp.FirstName,
                            LastName = emp.LastName,
                            Gender = emp.Gender,
                            DateOfBirth = emp.DateOfBirth,
                            Email = emp.Email,
                            PhoneNumber = emp.PhoneNumber,
                            Address = emp.Address,
                            HireDate = emp.HireDate,
                            JobTitleId = emp.JobTitleId,
                            JobName = job.Title ?? string.Empty,
                            DepartmentId = emp.DepartmentId,
                            DepartmentName = dep.DepartmentName ?? string.Empty,
                            ManagerId = emp.ManagerId,
                            ManagerName = mgr != null ? mgr.FirstName + " " + mgr.LastName : null, // Ambil nama manager
                            EmploymentStatus = emp.EmploymentStatus,
                            Salary = emp.Salary,
                            IsActive = emp.IsActive,
                            CreatedBy = emp.CreatedBy,
                            CreatedByName = emp.CreatedByName,
                            CreatedDate = emp.CreatedDate,
                            ModifiedBy = emp.ModifiedBy,
                            ModifiedByName = emp.ModifiedByName,
                            ModifiedDate = emp.ModifiedDate,
                        };

            return query;
        }
    }
}
