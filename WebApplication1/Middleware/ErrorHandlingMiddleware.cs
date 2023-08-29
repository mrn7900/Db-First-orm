
using System.Net;
using System.Text.Json;

namespace HeroApi.middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception here if required

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new ErrorResponse
                {
                    Message = "An error occurred",
                    Details = ex.Message
                };

                var jsonErrorResponse = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(jsonErrorResponse);
            }
        }
    }
}
