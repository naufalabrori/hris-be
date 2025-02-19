using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Extensions.Logging
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Lanjutkan ke middleware berikutnya

                // Log hanya jika request berhasil (status code 200-299)
                if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                {
                    _logger.LogInformation("Request success: {Method} {Path} - {StatusCode}",
                        context.Request.Method,
                        context.Request.Path,
                        context.Response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Request failed: {Method} {Path}",
                        context.Request.Method,
                        context.Request.Path);
                _logger.LogError(ex, "Terjadi kesalahan: {Message}", ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(new
                {
                    Success = false,
                    Message = ex.Message ?? string.Empty,
                });
            }
        }
    }

}
