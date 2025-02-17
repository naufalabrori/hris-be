
namespace HRIS.Core.Dto
{
    public record EmployeeTrainingDto(string employeeId, string trainingId, string status);

    public record EmployeeTrainingQueryDto(string employeeId, string trainingId, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record EmployeeTrainingsResponseDto(List<EmployeeTraining> Data, int TotalData);
}
