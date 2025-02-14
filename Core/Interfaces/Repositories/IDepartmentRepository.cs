
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        public Task AddAsync(Department department, CancellationToken cancellationToken);
        public Task<Department?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<Department?> GetByDepartmentNameAsync(string departmentName, CancellationToken cancellationToken);
        public Task<DepartmentsResponseDto> GetAllAsync(DepartmentQueryDto departmentQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Department department, CancellationToken cancellationToken);
        public Task DeleteAsync(Department department, CancellationToken cancellationToken);
    }
}
