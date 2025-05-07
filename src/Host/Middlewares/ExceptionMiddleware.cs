using Application.RP;
using System.Net;

namespace Host.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Excepción no controlada.");
                var result = Result<Unity>.Failure(HttpStatusCode.InternalServerError, Errors.UNHANDLED_ERROR);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)result.StatusCode;
                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
