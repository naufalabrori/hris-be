using System;

namespace HRIS.Core.Dto
{
    public record TrainingDto(string trainingName, string description, DateTime startDate, DateTime endDate, string trainer);
}
