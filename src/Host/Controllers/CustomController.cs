using Application.RP;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    public abstract class CustomController(IMediator mediator) : ControllerBase
    {
        protected IMediator Mediator => mediator;

        protected ObjectResult BuildResponse<T>(Result<T> result)
        {
            return StatusCode((int)result.StatusCode, result);
        }

        protected ObjectResult BuildResponse(Result<Unity> result)
        {
            object? body = result.Succeeded ? null : result;
            return StatusCode((int)result.StatusCode, body);
        }

        protected ObjectResult BuildResponse<T>(T value, int statusCode = StatusCodes.Status200OK)
        {
            return StatusCode(statusCode, Result<T>.Success(value));
        }
    }
}
