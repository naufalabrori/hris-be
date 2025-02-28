
namespace HRIS.Core.Dto
{
    public record DepartmentDto(string departmentName, string managerId, string location);

    public record DepartmentUpdateDto(string departmentName, string managerId, string location, bool? isActive);

    public record DepartmentQueryDto(string departmentName, string managerId, string managerName, string location, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public class DepartmenExtDto : Department
    {
        public string ManagerName { get; set; } = string.Empty;
    }

    public record DepartmentsResponseDto(List<DepartmenExtDto> Data, int TotalData);
}
