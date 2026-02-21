using CreditService.Application.DTO.Cibils;
using CreditService.Domain.Common;

namespace CreditService.Application.Helpers
{
    public static class ResponseHelper
    {
        public static ApiResponse<T> Success<T>(string message, T data)
        {
            return new ApiResponse<T>
            {
                success = true,
                message = message,
                data = data,
                error = null
            };
        }

        public static ApiResponse<object> Error(string message, string code, string details)
        {
            return new ApiResponse<object>
            {
                success = false,
                message = message,
                data = null,
                error = new ApiError
                {
                    code = code,
                    details = details
                }
            };
        }

        
    }
}
