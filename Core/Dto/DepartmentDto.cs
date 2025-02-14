
namespace HRIS.Core.Dto
{
    public record DepartmentDto(string departmentName, string managerId, string location);

    public record DepartmentQueryDto(string departmentName, string managerId, string location, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record DepartmentsResponseDto(List<Department> Data, int TotalData);
}
