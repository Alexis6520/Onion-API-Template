using Application.Commands.Donuts;
using Application.Queries.Donuts;
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

        [HttpGet]
        public async Task<IActionResult> GetList(int page = 1, int pageSize = 5)
        {
            var query = new DonutsListQuery
            {
                Page = page,
                PageSize = pageSize
            };

            var result = await Mediator.Send(query);
            return BuildResponse(result);
        }
    }
}
