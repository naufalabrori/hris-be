
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        public Task AddAsync(Employee employee, CancellationToken cancellationToken);
        public Task<Employee?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<EmployeesResponseDto> GetAllAsync(EmployeeQueryDto employeeQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Employee employee, CancellationToken cancellationToken);
        public Task DeleteAsync(Employee employee, CancellationToken cancellationToken);
    }
}
