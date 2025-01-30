using Application.Commands;
using Application.Commands.Donuts;
using Application.Models;
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
        /// <summary>
        /// Crea una donita del Krispy Kreme :)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType<Result<int>>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateDonutCommand command)
        {
            var result = await Mediator.Send(command);
            return BuildResponse(result);
        }

        /// <summary>
        /// Obtiene la lista de donitas
        /// </summary>
        /// <param name="page">Número de página</param>
        /// <param name="pageSize">Tamaño de página</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType<Result<List<DonutListItem>>>(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Obtiene una donita por Id
        /// </summary>
        /// <param name="id">Id de la donita</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType<Result<DonutDTO>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new FindQuery<int, DonutDTO>(id));
            return BuildResponse(result);
        }

        /// <summary>
        /// Actualiza una donita
        /// </summary>
        /// <param name="id">Id de la donita</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, UpdateDonutCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command);
            return BuildResponse(result);
        }

        /// <summary>
        /// Elimina una donita
        /// </summary>
        /// <param name="id">Id de la donita</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteCommand<int, Donut>(id));
            return BuildResponse(result);
        }
    }
}
