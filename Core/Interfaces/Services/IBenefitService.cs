
namespace HRIS.Core.Interfaces.Services
{
    public interface IBenefitService
    {
        public Task<ApiResponseDto<Benefits?>> CreateBenefitAsync(BenefitDto benefit, CancellationToken cancellationToken);
        public Task<ApiResponseDto<BenefitsResponseDto>> ReadBenefitsAsync(BenefitQueryDto benefitQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Benefits?>> ReadBenefitByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Benefits?>> UpdateBenefitAsync(string id, BenefitDto updateBenefit, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteBenefitAsync(string id, CancellationToken cancellationToken);
    }
}
