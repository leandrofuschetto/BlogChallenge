using BlogChallenge.Domain.Exceptions;
using System.Net;
using System.Reflection;
using System.Text.Json;

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
            response.ContentType = "application/json";

            switch (error)
            {
                case ArgumentNullException:
                case BadHttpRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case EntityNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case DataBaseContextException:
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer
                .Serialize(new { message = error?.Message, code = customErrorCode });

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
