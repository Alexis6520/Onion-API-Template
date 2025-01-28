using Application.Abstractions;
using Application.Commands;
using Application.Models;
using Domain.Entities;
using Services;
using System.Net;

namespace Application.Handlers.Donuts
{
    /// <summary>
    /// Manejador de eliminación de dona
    /// </summary>
    /// <param name="dbContext">Servicio de base de datos</param>
    public class DeleteDonutHandler(AppDbContext dbContext) : IRequestHandler<DeleteCommand<int, Donut>>
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Result> Handle(DeleteCommand<int, Donut> request, CancellationToken cancellationToken)
        {
            var donut = await _dbContext.Donuts
                .FindAsync([request.Key], cancellationToken);

            if (donut == null)
                return Result.Fail(HttpStatusCode.NotFound, "Dona no encontrada");

            _dbContext.Donuts.Remove(donut);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
