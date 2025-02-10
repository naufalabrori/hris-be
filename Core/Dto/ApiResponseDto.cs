
namespace HRIS.Core.Dto
{
    public class ApiResponseDto<T>
    {
        public ApiResponseDto() { }

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ApiResponseDto(bool success, string message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
