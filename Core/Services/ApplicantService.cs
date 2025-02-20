
namespace HRIS.Core.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IHrisRepository _hrisRepository;
        private readonly IApplicantRepository _applicantRepository;

        public ApplicantService(IHrisRepository hrisRepository, IApplicantRepository applicantRepository)
        {
            _hrisRepository = hrisRepository;
            _applicantRepository = applicantRepository;
        }

        public async Task<ApiResponseDto<Applicant?>> CreateApplicantAsync(ApplicantDto applicant, CancellationToken cancellationToken)
        {
            var newApplicant = new Applicant(applicant);

            await _applicantRepository.AddAsync(newApplicant, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Applicant?>
            {
                Success = true,
                Message = "Create applicant successfully",
                Data = newApplicant
            };
        }

        public async Task<ApiResponseDto<ApplicantsResponseDto>> ReadApplicantsAsync(ApplicantQueryDto applicantQueryDto, CancellationToken cancellationToken)
        {
            var data = await _applicantRepository.GetAllAsync(applicantQueryDto, cancellationToken);

            return new ApiResponseDto<ApplicantsResponseDto>
            {
                Success = true,
                Message = "Get all applicant successfully",
                Data = data
            };
        }

        public async Task<ApiResponseDto<Applicant?>> ReadApplicantByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Applicant?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var applicant = await _applicantRepository.GetByIdAsync(id, cancellationToken);
            if (applicant == null)
            {
                return new ApiResponseDto<Applicant?>
                {
                    Success = false,
                    Message = "Applicant not found"
                };
            }

            return new ApiResponseDto<Applicant?>
            {
                Success = true,
                Message = "Get applicant successfully",
                Data = applicant
            };
        }

        public async Task<ApiResponseDto<Applicant?>> UpdateApplicantAsync(string id, ApplicantDto updateApplicant, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Applicant?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var applicant = await _applicantRepository.GetByIdAsync(id, cancellationToken);
            if (applicant == null)
            {
                return new ApiResponseDto<Applicant?>
                {
                    Success = false,
                    Message = "Applicant not found"
                };
            }

            applicant.UpdateApplicant(updateApplicant);

            await _applicantRepository.UpdateAsync(applicant, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Applicant?>
            {
                Success = true,
                Message = "Update applicant successfully",
                Data = applicant
            };
        }

        public async Task<ApiResponseDto<bool>> DeleteApplicantAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var applicant = await _applicantRepository.GetByIdAsync(id, cancellationToken);
            if (applicant == null)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Applicant not found"
                };
            }

            await _applicantRepository.DeleteAsync(applicant, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<bool>
            {
                Success = true,
                Message = "Delete applicant successfully",
                Data = true
            };
        }
    }
}
