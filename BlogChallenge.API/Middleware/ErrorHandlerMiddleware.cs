using BlogChallenge.Domain.Exceptions;
using System.Net;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace BlogChallenge.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await HandleExceptionAsync(context, error);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            var customErrorCode = GetCustomPropertyCode(error);

            var response = context.Response;
            response.ContentType = "application/problem+json";

            string title;
            string type;

            switch (error)
            {
                case ArgumentNullException:
                case BadHttpRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    title = "Bad Request";
                    type = "https://httpstatuses.com/400";
                    break;
                case EntityNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    title = "Not Found";
                    type = "https://httpstatuses.com/404";
                    break;
                case DataBaseContextException:
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    title = "Internal Server Error";
                    type = "https://httpstatuses.com/500";
                    break;
            }

            var problem = new ProblemDetails
            {
                Title = title,
                Type = type,
                Status = response.StatusCode,
                Detail = error?.Message,
                Instance = context.Request?.Path.Value
            };

            problem.Extensions["code"] = customErrorCode;
            problem.Extensions["traceId"] = context.TraceIdentifier;

            var result = JsonSerializer.Serialize(problem);
            return response.WriteAsync(result);
        }

        private string GetCustomPropertyCode(Exception error)
        {
            var prop = error.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name == "Code")
                .FirstOrDefault();

            string code = "GENERAL_ERROR";
            if (prop != null)
                code = prop.GetValue(error, null).ToString();

            return code;
        }
    }
}
