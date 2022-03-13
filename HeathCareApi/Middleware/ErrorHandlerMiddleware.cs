using HealthCareApi.Exceptions;
using System.Net;
using System.Text.Json;

namespace HealthCareApi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);   
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = error switch
                {
                    BadRequestException => (int)HttpStatusCode.BadRequest, //custom applcation error
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,// not found error
                    ForbiddenException => (int)HttpStatusCode.Forbidden,// unhandled error
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var result = JsonSerializer.Serialize(new {message = error?.Message});
                await response.WriteAsync(result);

            }
        }
    }
}
