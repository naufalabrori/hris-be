
namespace HRIS.Core.Services
{
    public class PerformanceReviewService : IPerformanceReviewService
    {
        private readonly IHrisRepository _hrisRepository;
        private readonly IPerformanceReviewRepository _performanceReviewRepository;

        public PerformanceReviewService(IHrisRepository hrisRepository, IPerformanceReviewRepository performanceReviewRepository)
        {
            _hrisRepository = hrisRepository;
            _performanceReviewRepository = performanceReviewRepository;
        }

        public async Task<ApiResponseDto<PerformanceReview?>> CreatePerformanceReviewAsync(PerformanceReviewDto performanceReview, CancellationToken cancellationToken)
        {
            var newPerformanceReview = new PerformanceReview(performanceReview);

            await _performanceReviewRepository.AddAsync(newPerformanceReview, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<PerformanceReview?>
            {
                Success = true,
                Message = "Create performance review successfully",
                Data = newPerformanceReview
            };
        }

        public async Task<ApiResponseDto<PerformanceReviewsResponseDto>> ReadPerformanceReviewsAsync(PerformanceReviewQueryDto performanceReviewQueryDto, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _performanceReviewRepository.GetAllAsync(performanceReviewQueryDto, cancellationToken);

                return new ApiResponseDto<PerformanceReviewsResponseDto>
                {
                    Success = true,
                    Message = "Get all performance review successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<PerformanceReviewsResponseDto>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<PerformanceReview?>> ReadPerformanceReviewByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<PerformanceReview?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var performanceReview = await _performanceReviewRepository.GetByIdAsync(id, cancellationToken);
                if (performanceReview == null)
                {
                    return new ApiResponseDto<PerformanceReview?>
                    {
                        Success = false,
                        Message = "Performance review not found"
                    };
                }

                return new ApiResponseDto<PerformanceReview?>
                {
                    Success = true,
                    Message = "Get performance review successfully",
                    Data = performanceReview
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<PerformanceReview?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<PerformanceReview?>> UpdatePerformanceReviewAsync(string id, PerformanceReviewDto updatePerformanceReview, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<PerformanceReview?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var performanceReview = await _performanceReviewRepository.GetByIdAsync(id, cancellationToken);
                if (performanceReview == null)
                {
                    return new ApiResponseDto<PerformanceReview?>
                    {
                        Success = false,
                        Message = "Performance review not found"
                    };
                }

                performanceReview.UpdatePerformanceReview(updatePerformanceReview);

                await _performanceReviewRepository.UpdateAsync(performanceReview, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<PerformanceReview?>
                {
                    Success = true,
                    Message = "Update performance review successfully",
                    Data = performanceReview
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<PerformanceReview?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<bool>> DeletePerformanceReviewAsync(string id, CancellationToken cancellationToken)
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

                var performanceReview = await _performanceReviewRepository.GetByIdAsync(id, cancellationToken);
                if (performanceReview == null)
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Performance review not found"
                    };
                }

                await _performanceReviewRepository.DeleteAsync(performanceReview, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Message = "Delete performance review successfully",
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
