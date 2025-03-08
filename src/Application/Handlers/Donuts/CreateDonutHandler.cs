using Application.Abstractions;
using Application.Commands.Donuts;
using Application.Models;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Domain.Services;
using System.Net;

namespace Application.Handlers.Donuts
{
    /// <summary>
    /// Manejador de creación de donitas del Krispy Kreme
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="logger">Servicio de logs</param>
    public class CreateDonutHandler(AppDbContext dbContext, ILogger<CreateDonutHandler> logger) : IRequestHandler<CreateDonutCommand, int>
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly ILogger<CreateDonutHandler> _logger = logger;

        public async Task<Result<int>> Handle(CreateDonutCommand request, CancellationToken cancellationToken)
        {
            var donut = new Donut
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
            };

            _dbContext.Donuts.Add(donut);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Dona {Id} creada", donut.Id);
            return Result<int>.Success(donut.Id, HttpStatusCode.Created);
        }
    }
}
