
namespace HRIS.Core.Services
{
    public class RecruitmentService : IRecruitmentService
    {
        private readonly IHrisRepository _hrisRepository;
        private readonly IRecruitmentRepository _recruitmentRepository;
        private readonly IJobTitleRepository _jobTitleRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public RecruitmentService(IHrisRepository hrisRepository, IRecruitmentRepository recruitmentRepository, IJobTitleRepository jobTitleRepository, IDepartmentRepository departmentRepository)
        {
            _hrisRepository = hrisRepository;
            _recruitmentRepository = recruitmentRepository;
            _jobTitleRepository = jobTitleRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<ApiResponseDto<Recruitment?>> CreateRecruitmentAsync(RecruitmentDto recruitment, CancellationToken cancellationToken)
        {
            try
            {
                var existingJob = await _jobTitleRepository.GetByIdAsync(recruitment.jobTitleId, cancellationToken);
                if (existingJob == null)
                {
                    return new ApiResponseDto<Recruitment?>
                    {
                        Success = false,
                        Message = "Job title not found"
                    };
                }

                var existingDepartment = await _departmentRepository.GetByIdAsync(recruitment.departmentId, cancellationToken);
                if (existingDepartment == null)
                {
                    return new ApiResponseDto<Recruitment?>
                    {
                        Success = false,
                        Message = "Department not found"
                    };
                }

                var newRecruitment = new Recruitment(recruitment);

                await _recruitmentRepository.AddAsync(newRecruitment, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<Recruitment?>
                {
                    Success = true,
                    Message = "Create recruitment successfully",
                    Data = newRecruitment
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Recruitment?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<RecruitmentsResponseDto>> ReadRecruitmentsAsync(RecruitmentQueryDto recruitmentQueryDto, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _recruitmentRepository.GetAllAsync(recruitmentQueryDto, cancellationToken);

                return new ApiResponseDto<RecruitmentsResponseDto>
                {
                    Success = true,
                    Message = "Get all recruitment successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<RecruitmentsResponseDto>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<Recruitment?>> ReadRecruitmentByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<Recruitment?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var recruitment = await _recruitmentRepository.GetByIdAsync(id, cancellationToken);
                if (recruitment == null)
                {
                    return new ApiResponseDto<Recruitment?>
                    {
                        Success = false,
                        Message = "Recruitment not found"
                    };
                }

                return new ApiResponseDto<Recruitment?>
                {
                    Success = true,
                    Message = "Get recruitment successfully",
                    Data = recruitment
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Recruitment?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<Recruitment?>> UpdateRecruitmentAsync(string id, RecruitmentDto updateRecruitment, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<Recruitment?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var recruitment = await _recruitmentRepository.GetByIdAsync(id, cancellationToken);
                if (recruitment == null)
                {
                    return new ApiResponseDto<Recruitment?>
                    {
                        Success = false,
                        Message = "Recruitment not found"
                    };
                }

                recruitment.UpdateRecruitment(updateRecruitment);

                await _recruitmentRepository.UpdateAsync(recruitment, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<Recruitment?>
                {
                    Success = true,
                    Message = "Update recruitment successfully",
                    Data = recruitment
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Recruitment?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteRecruitmentAsync(string id, CancellationToken cancellationToken)
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

                var recruitment = await _recruitmentRepository.GetByIdAsync(id, cancellationToken);
                if (recruitment == null)
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Recruitment not found"
                    };
                }

                await _recruitmentRepository.DeleteAsync(recruitment, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Message = "Delete recruitment successfully",
                    Data = true,
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
