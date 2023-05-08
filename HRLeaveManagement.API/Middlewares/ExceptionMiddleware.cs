using HRLeaveManagement.API.Models;
using HRLeaveManagement.Application.Exceptions;
using System.Net;

namespace HRLeaveManagement.API.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            CustomValidationProbemDetails problem;

            switch (ex)
            {
                case BadRequestException BadRequest:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomValidationProbemDetails
                    {
                        Title = BadRequest.Message,
                        Status = (int)statusCode,
                        Detail = BadRequest.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = BadRequest.ValidationErrors,
                    };
                    break;
                case NotFoundException NotFound:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomValidationProbemDetails
                    {
                        Title = NotFound.Message,
                        Status = (int)statusCode,
                        Detail = NotFound.InnerException?.Message,
                        Type = nameof(NotFoundException),
                    };
                    break;
                default:
                    problem = new CustomValidationProbemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Detail = ex.StackTrace,
                        Type = nameof(HttpStatusCode.InternalServerError),
                    };
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
