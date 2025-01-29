using Application.Models;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

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
                logger.LogError(ex, "Excepción no controlada");

                var result = Result.Fail(
                    HttpStatusCode.InternalServerError,
                    "Hubo un problema al procesar su solicitud");

                var response = context.Response;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.ContentType = MediaTypeNames.Application.Json;
                var json = JsonSerializer.Serialize(result);
                await response.WriteAsync(json);
            }
        }
    }
}
