
namespace HRIS.Core.Services
{
    public class BenefitService : IBenefitService
    {
        private readonly IHrisRepository _hrisRepository;
        private readonly IBenefitRepository _benefitRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BenefitService(IHrisRepository hrisRepository, IBenefitRepository benefitRepository, IEmployeeRepository employeeRepository)
        {
            _hrisRepository = hrisRepository;
            _benefitRepository = benefitRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<ApiResponseDto<Benefits?>> CreateBenefitAsync(BenefitDto benefit, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(benefit.employeeId, cancellationToken);
            if (existingEmployee == null)
            {
                return new ApiResponseDto<Benefits?>
                {
                    Success = false,
                    Message = "Employee not found",
                };
            }

            var existingBenefit = await _benefitRepository.GetByEmployeeIdAsync(benefit.employeeId, cancellationToken);
            if (existingBenefit.Count > 0)
            {
                existingBenefit = existingBenefit.Where(x => x.BenefitType == benefit.benefitType && x.StartDate.Date == benefit.startDate.Date && x.EndDate.Date == benefit.endDate.Date).ToList();

                if (existingBenefit.Count > 0)
                {
                    return new ApiResponseDto<Benefits?>
                    {
                        Success = false,
                        Message = $"Benefit already exist for this employee for {benefit.benefitType} in {benefit.startDate.Date} into {benefit.endDate.Date}",
                    };
                }
            }

            var newBenefit = new Benefits(benefit);

            await _benefitRepository.AddAsync(newBenefit, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Benefits?>
            {
                Success = true,
                Message = "Create benefit successfully",
                Data = newBenefit
            };
        }

        public async Task<ApiResponseDto<BenefitsResponseDto>> ReadBenefitsAsync(BenefitQueryDto benefitQueryDto, CancellationToken cancellationToken)
        {
            var data = await _benefitRepository.GetAllAsync(benefitQueryDto, cancellationToken);

            return new ApiResponseDto<BenefitsResponseDto>
            {
                Success = true,
                Message = "Get all benefit successfully",
                Data = data
            };
        }

        public async Task<ApiResponseDto<Benefits?>> ReadBenefitByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Benefits?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var benefit = await _benefitRepository.GetByIdAsync(id, cancellationToken);
            if (benefit == null)
            {
                return new ApiResponseDto<Benefits?>
                {
                    Success = false,
                    Message = "Benefit not found"
                };
            }

            return new ApiResponseDto<Benefits?>
            {
                Success = true,
                Message = "Get benefit successfully",
                Data = benefit
            };
        }

        public async Task<ApiResponseDto<Benefits?>> UpdateBenefitAsync(string id, BenefitDto updateBenefit, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Benefits?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var benefit = await _benefitRepository.GetByIdAsync(id, cancellationToken);
            if (benefit == null)
            {
                return new ApiResponseDto<Benefits?>
                {
                    Success = false,
                    Message = "Benefit not found"
                };
            }

            benefit.UpdateBenefits(updateBenefit);

            await _benefitRepository.UpdateAsync(benefit, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Benefits?>
            {
                Success = true,
                Message = "Update benefit successfully",
                Data = benefit
            };
        }

        public async Task<ApiResponseDto<bool>> DeleteBenefitAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var benefit = await _benefitRepository.GetByIdAsync(id, cancellationToken);
            if (benefit == null)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Benefit not found"
                };
            }

            await _benefitRepository.DeleteAsync(benefit, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<bool>
            {
                Success = true,
                Message = "Delete benefit successfully",
                Data = true,
            };
        }
    }
}
