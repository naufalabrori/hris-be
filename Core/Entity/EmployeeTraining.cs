using HRIS.Core.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class EmployeeTraining : BaseEntity
    {
        public EmployeeTraining() { }

        public EmployeeTraining(EmployeeTrainingDto employeeTraining)
        {
            EmployeeId = Guid.Parse(employeeTraining.employeeId);
            TrainingId = Guid.Parse(employeeTraining.trainingId);
            Status = employeeTraining.status;
        }

        public Guid Id { get; set; } = default!;
        public Guid EmployeeId { get; set; } = default!;
        public Guid TrainingId { get; set; } = default!;
        [StringLength(50)]
        public string Status { get; set; } = default!;

        public void UpdateEmployeeTraining(EmployeeTrainingDto employeeTraining)
        {
            EmployeeId = Guid.TryParse(employeeTraining.employeeId, out var employeeId) ? employeeId : EmployeeId;
            TrainingId = Guid.TryParse(employeeTraining.trainingId, out var trainingId) ? trainingId : TrainingId;
            Status = employeeTraining.status ?? Status;
        }
    }
}
