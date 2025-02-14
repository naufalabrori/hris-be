
namespace HRIS.Core.Dto
{
    public record EmployeeDto(string firstName, string lastName, string gender, DateTime? dateOfBirth, string email, string phoneNumber, string address, DateTime? hireDate, string jobTitleId,
        string departmentId, string managerId, string employmentStatus, decimal salary);

    public record EmployeeQueryDto(string firstName, string lastName, string gender, DateTime? dateOfBirth, string email, string phoneNumber, string address, DateTime? hireDate, string jobTitleId,
        string departmentId, string managerId, string employmentStatus, decimal salary, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record EmployeesResponseDto(List<Employee> Data, int TotalData);
}
