
namespace HRIS.Core.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IHrisRepository _hrisRepository;

        public PayrollService(IPayrollRepository payrollRepository, IHrisRepository hrisRepository)
        {
            _payrollRepository = payrollRepository;
            _hrisRepository = hrisRepository;
        }

        public async Task<ApiResponseDto<Payroll?>> CreatePayrollAsync(PayrollDto payroll, CancellationToken cancellationToken)
        {
            try
            {
                var existingPayroll = await _payrollRepository.GetByEmployeeIdAsync(payroll.employeeId, cancellationToken);
                if (existingPayroll.Count > 0)
                {
                    existingPayroll = existingPayroll.Where(x => x.PayPeriodStartDate.Date == payroll.payPeriodStartDate.Date && x.PayPeriodEndDate.Date == payroll.payPeriodEndDate.Date).ToList();

                    if (existingPayroll.Count > 0)
                    {
                        return new ApiResponseDto<Payroll?>
                        {
                            Success = false,
                            Message = "Payroll already exist"
                        };
                    }
                }

                var newPayroll = new Payroll(payroll);

                await _payrollRepository.AddAsync(newPayroll, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<Payroll?>
                {
                    Success = true,
                    Message = "Create payroll successfully",
                    Data = newPayroll
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Payroll?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<PayrollsResponseDto>> ReadPayrollsAsync(PayrollQueryDto payrollQueryDto, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _payrollRepository.GetAllAsync(payrollQueryDto, cancellationToken);

                return new ApiResponseDto<PayrollsResponseDto>
                {
                    Success = true,
                    Message = "Get all payroll successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<PayrollsResponseDto>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<Payroll?>> ReadPayrollByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<Payroll?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var payroll = await _payrollRepository.GetByIdAsync(id, cancellationToken);
                if (payroll == null)
                {
                    return new ApiResponseDto<Payroll?>
                    {
                        Success = false,
                        Message = "Payroll not found",
                    };
                }

                return new ApiResponseDto<Payroll?>
                {
                    Success = true,
                    Message = "Get payroll successfully",
                    Data = payroll
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Payroll?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<Payroll?>> UpdatePayrollAsync(string id, PayrollDto updatePayroll, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<Payroll?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var payroll = await _payrollRepository.GetByIdAsync(id, cancellationToken);
                if (payroll == null)
                {
                    return new ApiResponseDto<Payroll?>
                    {
                        Success = false,
                        Message = "Payroll not found",
                    };
                }

                payroll.UpdatePayroll(updatePayroll);

                await _payrollRepository.UpdateAsync(payroll, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<Payroll?>
                {
                    Success = true,
                    Message = "Update payroll successfully",
                    Data = payroll
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Payroll?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<bool>> DeletePayrollAsync(string id, CancellationToken cancellationToken)
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

                var payroll = await _payrollRepository.GetByIdAsync(id, cancellationToken);
                if (payroll == null)
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Payroll not found",
                    };
                }

                await _payrollRepository.DeleteAsync(payroll, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Message = "Delete payroll successfully",
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
