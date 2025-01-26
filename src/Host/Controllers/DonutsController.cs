using Application.Commands.Donuts;
using Host.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    public class DonutsController(IMediator mediator) : CustomController(mediator)
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateDonutCommand command)
        {
            var result = await Mediator.Send(command);
            return BuildResponse(result);
        }
    }
}
