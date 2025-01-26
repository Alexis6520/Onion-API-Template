using Application.Abstractions;
using Application.Commands.Donuts;
using Application.Models;
using Domain.Entities;
using Services;
using System.Net;

namespace Application.Handlers.Donuts
{
    /// <summary>
    /// Manejador de creación de donitas del Krispy Kreme
    /// </summary>
    /// <param name="dbContext"></param>
    public class CreateDonutHandler(AppDbContext dbContext) : IRequestHandler<CreateDonutCommand, int>
    {
        private readonly AppDbContext _dbContext = dbContext;

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
            return Result<int>.Success(donut.Id, HttpStatusCode.Created);
        }
    }
}
