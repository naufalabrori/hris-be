
namespace HRIS.Core.Dto
{
    public record TrainingDto(string trainingName, string description, DateTime startDate, DateTime endDate, string trainer);

    public record TrainingQueryDto(string trainingName, string description, DateTime startDate, DateTime endDate, string trainer, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record TrainingsResponseDto(List<Training> Data, int TotalData);
}
