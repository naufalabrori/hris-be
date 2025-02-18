using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class Employee : BaseEntity
    {
        public Employee() { }

        public Employee(EmployeeDto employee)
        {
            FirstName = employee.firstName;
            LastName = employee.lastName;
            Gender = employee.gender;
            DateOfBirth = employee.dateOfBirth;
            Email = employee.email;
            PhoneNumber = employee.phoneNumber;
            Address = employee.address;
            HireDate = employee.hireDate;
            JobTitleId = Guid.Parse(employee.jobTitleId);
            DepartmentId = Guid.Parse(employee.departmentId);
            ManagerId = Guid.TryParse(employee.managerId, out var managerIdGuid) ? managerIdGuid : Guid.Empty;
            EmploymentStatus = employee.employmentStatus;
            Salary = employee.salary;
        }

        public Guid Id { get; set; } = default!;
        [StringLength(50)]
        public string FirstName { get; set; } = default!;
        [StringLength(50)]
        public string? LastName { get; set; } = string.Empty;
        [StringLength(1)]
        public string? Gender { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; } = null;
        [StringLength(100)]
        public string? Email { get; set; } = string.Empty;
        [StringLength(16)]
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public DateTime? HireDate { get; set; } = null;
        public Guid JobTitleId {  get; set; } = default!;
        public Guid DepartmentId { get; set; } = default!;
        public Guid ManagerId { get; set; } = default!;
        [StringLength(50)]
        public string EmploymentStatus { get; set; } = default!;
        public decimal? Salary { get; set; }

        public void UpdateEmployee(EmployeeDto employee)
        {
            FirstName = employee.firstName ?? FirstName;
            LastName = employee.lastName ?? LastName;
            Gender = employee.gender ?? Gender;
            DateOfBirth = employee.dateOfBirth ?? DateOfBirth;
            Email = employee.email ?? Email;
            PhoneNumber = employee.phoneNumber ?? PhoneNumber;
            Address = employee.address ?? Address;
            HireDate = employee.hireDate ?? HireDate;
            JobTitleId = Guid.TryParse(employee.jobTitleId, out var jobTitleIdGuid) ? jobTitleIdGuid : JobTitleId;
            DepartmentId = Guid.TryParse(employee.departmentId, out var departmentIdGuid) ? departmentIdGuid : DepartmentId;
            ManagerId = Guid.TryParse(employee.managerId, out var managerIdGuid) ? managerIdGuid : ManagerId;
            EmploymentStatus = employee.employmentStatus ?? EmploymentStatus;
            Salary = employee?.salary ?? Salary;
        }

    }
}
