using Application.Commands;
using Application.Commands.Donuts;
using Application.Models.Donuts;
using Application.Queries;
using Application.Queries.Donuts;
using Domain.Entities;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new FindQuery<int, DonutDTO>(id));
            return BuildResponse(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, UpdateDonutCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command);
            return BuildResponse(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteCommand<int, Donut>(id));
            return BuildResponse(result);
        }
    }
}
