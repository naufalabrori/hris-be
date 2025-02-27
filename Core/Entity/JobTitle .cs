using HRIS.Core.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class JobTitle : BaseEntity
    {
        public JobTitle() { }

        public JobTitle(JobTitleDto jobTitleDto)
        {
            Title = jobTitleDto.title;
            Description = jobTitleDto.description;
            MinSalary = jobTitleDto.minSalary;
            MaxSalary = jobTitleDto.maxSalary;
        }

        public Guid Id { get; set; } = default!;
        [StringLength(100)]
        public string Title { get; set; } = default!;
        public string? Description { get; set; } = string.Empty;
        public decimal? MinSalary { get; set; } 
        public decimal? MaxSalary { get; set; }

        public void UpdateJobTitle(JobTitleUpdateDto jobTitleDto)
        {
            Title = jobTitleDto.title ?? Title;
            Description = jobTitleDto.description ?? Description;
            MinSalary = jobTitleDto?.minSalary ?? MinSalary;
            MaxSalary = jobTitleDto?.maxSalary ?? MaxSalary;
            IsActive = jobTitleDto?.isActive ?? IsActive;
        }
    }
}
