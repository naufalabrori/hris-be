﻿
namespace HRIS.Core.Services
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleRepository _jobTitleRepository;
        private readonly IHrisRepository _hrisRepository;

        public JobTitleService(IJobTitleRepository jobTitleRepository, IHrisRepository hrisRepository)
        {
            _jobTitleRepository = jobTitleRepository;
            _hrisRepository = hrisRepository;
        }

        public async Task<ApiResponseDto<JobTitle?>> CreateJobTitleAsync(JobTitleDto jobTitle, CancellationToken cancellationToken)
        {
            try
            {
                var newJobTitle = new JobTitle(jobTitle);

                await _jobTitleRepository.AddAsync(newJobTitle, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<JobTitle?>
                {
                    Success = true,
                    Message = "Create job title successfully",
                    Data = newJobTitle
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<JobTitle?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<JobTitlesResponseDto>> ReadJobTitlesAsync(JobTitleQueryDto jobTitleQueryDto, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _jobTitleRepository.GetAllAsync(jobTitleQueryDto, cancellationToken);

                return new ApiResponseDto<JobTitlesResponseDto>
                {
                    Success = true,
                    Message = "Get all job title successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<JobTitlesResponseDto>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<JobTitle?>> ReadJobTitleByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<JobTitle?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var jobTitle = await _jobTitleRepository.GetByIdAsync(id, cancellationToken);
                if (jobTitle == null)
                {
                    return new ApiResponseDto<JobTitle?>
                    {
                        Success = false,
                        Message = "Job title not found",
                    };
                }

                return new ApiResponseDto<JobTitle?>
                {
                    Success = true,
                    Message = "Get job title successfully",
                    Data = jobTitle
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<JobTitle?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<JobTitle?>> UpdateJobTitleAsync(string id, JobTitleDto updateJobTitle, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<JobTitle?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var jobTitle = await _jobTitleRepository.GetByIdAsync(id, cancellationToken);
                if (jobTitle == null)
                {
                    return new ApiResponseDto<JobTitle?>
                    {
                        Success = false,
                        Message = "Job title not found",
                    };
                }

                jobTitle.UpdateJobTitle(updateJobTitle);

                await _jobTitleRepository.UpdateAsync(jobTitle, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<JobTitle?>
                {
                    Success = true,
                    Message = "Update job title successfully",
                    Data = jobTitle
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<JobTitle?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteJobTitleAsync(string id, CancellationToken cancellationToken)
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

                var jobTitle = await _jobTitleRepository.GetByIdAsync(id, cancellationToken);
                if (jobTitle == null)
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Job title not found",
                    };
                }

                await _jobTitleRepository.DeleteAsync(jobTitle, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Message = "Delete job title successfully",
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
