
namespace HRIS.Core.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IHrisRepository _hrisRepository;

        public TrainingService(ITrainingRepository trainingRepository, IHrisRepository hrisRepository)
        {
            _trainingRepository = trainingRepository;
            _hrisRepository = hrisRepository;
        }

        public async Task<ApiResponseDto<Training?>> CreateTrainingAsync(TrainingDto training, CancellationToken cancellationToken)
        {
            try
            {
                var newTraining = new Training(training);

                await _trainingRepository.AddAsync(newTraining, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<Training?>
                {
                    Success = true,
                    Message = "Create training successfully",
                    Data = newTraining
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Training?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<TrainingsResponseDto>> ReadTrainingsAsync(TrainingQueryDto trainingQueryDto, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _trainingRepository.GetAllAsync(trainingQueryDto, cancellationToken);

                return new ApiResponseDto<TrainingsResponseDto>
                {
                    Success = true,
                    Message = "Get all training successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<TrainingsResponseDto>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ApiResponseDto<Training?>> ReadTrainingByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<Training?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var training = await _trainingRepository.GetByIdAsync(id, cancellationToken);
                if (training == null)
                {
                    return new ApiResponseDto<Training?>
                    {
                        Success = false,
                        Message = "Training not found",
                    };
                }

                return new ApiResponseDto<Training?>
                {
                    Success = true,
                    Message = "Get training successfully",
                    Data = training
                };
            }
            catch(Exception ex)
            {
                return new ApiResponseDto<Training?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<Training?>> UpdateTrainingAsync(string id, TrainingDto updateTraining, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<Training?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var training = await _trainingRepository.GetByIdAsync(id, cancellationToken);
                if (training == null)
                {
                    return new ApiResponseDto<Training?>
                    {
                        Success = false,
                        Message = "Training not found",
                    };
                }

                training.UpdateTraining(updateTraining);

                await _trainingRepository.UpdateAsync(training, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<Training?>
                {
                    Success = true,
                    Message = "Update training successfully",
                    Data= training
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Training?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteTrainingAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var training = await _trainingRepository.GetByIdAsync(id, cancellationToken);
                if (training == null)
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Training not found",
                    };
                }

                await _trainingRepository.DeleteAsync(training, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Message = "Delete training successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
