using HRIS.Core.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class Training : BaseEntity
    {
        public Training() { }

        public Training(TrainingDto training)
        {
            TrainingName = training.trainingName;
            Description = training.description;
            StartDate = training.startDate;
            EndDate = training.endDate;
            Trainer = training.trainer;
        }

        public Guid Id { get; set; } = default!;
        [StringLength(100)]
        public string TrainingName { get; set; } = default!;
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
        [StringLength(100)]
        public string Trainer { get; set; } = default!;

        public void UpdateTraining(TrainingDto training)
        {
            TrainingName = training.trainingName ?? TrainingName;
            Description = training.description ?? Description;
            StartDate = training?.startDate ?? StartDate;
            EndDate = training?.endDate ?? EndDate;
            Trainer = training?.trainer ?? Trainer;
        }
    }
}
