
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

        public async Task<Employee?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid empId = Guid.Parse(id);
            var employee = await _hrisContext.Employees.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == empId, cancellationToken);
            return employee;
        }

        public async Task<EmployeesResponseDto> GetAllAsync(EmployeeQueryDto employeeQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.Employees.IsActiveRows().Select(x => x);

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
    }
}
