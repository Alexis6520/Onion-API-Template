using Application.Commands.Donuts;
using Application.Models.Donuts;
using Application.Queries;
using Application.Queries.Donuts;
using Application.RP;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    public class DonutsController(IMediator mediator) : CustomController(mediator)
    {
        [HttpPost]
        [ProducesResponseType<Result<int>>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateDonutCommand command)
        {
            return BuildResponse(await Mediator.Send(command));
        }

        [HttpGet]
        [ProducesResponseType<Result<List<DonutItem>>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList(int pageSize = 10)
        {
            var result = await Mediator.Send(new DonutListQuery(pageSize));
            return BuildResponse(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<Result<DonutDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new FindQuery<int, DonutDTO>(id));
            return BuildResponse(result);
        }
    }
}
