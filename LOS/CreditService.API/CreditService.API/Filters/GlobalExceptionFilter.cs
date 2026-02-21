using CreditService.Application.Helpers;
using LosLms.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace LosLms.API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Unhandled exception occurred");

            int statusCode = (int)HttpStatusCode.InternalServerError;
            string errorCode = "INTERNAL_ERROR";
            string message = "Something went wrong. Please try again later.";
            string details = context.Exception.Message.ToString();

            if (context.Exception is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                errorCode = "UNAUTHORIZED";
                message = "Unauthorized access.";
            }
            else if (context.Exception is KeyNotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                errorCode = "NOT_FOUND";
                message = "Resource not found.";
            }
            else if (context.Exception is ArgumentException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorCode = "BAD_REQUEST";
                message = "Invalid request.";
            }

            context.Result = new ObjectResult(
                ResponseHelper.Error(message, errorCode, details)
            )
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}
