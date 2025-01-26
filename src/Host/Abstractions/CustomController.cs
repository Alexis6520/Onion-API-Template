using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Abstractions
{
    /// <summary>
    /// Controlador base personalizado
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CustomController(IMediator mediator) : ControllerBase
    {
        protected IMediator Mediator => mediator;

        protected ObjectResult BuildResponse(Result result)
        {
            var statusCode = (int)result.StatusCode;

            if (!result.Succeeded)
                return StatusCode(statusCode, result);

            return StatusCode(statusCode, null);
        }

        protected ObjectResult BuildResponse<TValue>(Result<TValue> result)
        {
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
