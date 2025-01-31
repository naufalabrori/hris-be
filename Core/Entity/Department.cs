using HRIS.Core.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class Department : BaseEntity
    {
        public Department() { }

        public Department(DepartmentDto departmentDto)
        {
            DepartmentName = departmentDto.departmentName;
            ManagerId = Guid.Parse(departmentDto.managerId);
            Location = departmentDto.location;
        }

        public Guid Id { get; set; } = default!;
        [StringLength(100)]
        public string DepartmentName { get; set; } = default!;
        public Guid ManagerId { get; set; } = default!;
        [StringLength(255)]
        public string Location { get; set; } = default!;

        public void UpdateDepartment(DepartmentDto departmentDto)
        {
            DepartmentName = departmentDto.departmentName ?? DepartmentName;
            ManagerId = Guid.TryParse(departmentDto.managerId, out var managerIdGuid) ? managerIdGuid : ManagerId;
            Location = departmentDto.location ?? Location;
        }
    }
}
