using Application.Abstractions;
using Application.Models;
using Application.Models.Donuts;
using Application.Queries;
using Application.Queries.Donuts;
using Microsoft.EntityFrameworkCore;
using Domain.Services;
using System.Net;

namespace Application.Handlers.Donuts
{
    /// <summary>
    /// Manejador de consultas de dona
    /// </summary>
    /// <param name="dbContext">Servicio de base de datos</param>
    public class DonutQueryHandler(AppDbContext dbContext) :
        IRequestHandler<DonutsListQuery, List<DonutListItem>>,
        IRequestHandler<FindQuery<int, DonutDTO>, DonutDTO>
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Result<List<DonutListItem>>> Handle(DonutsListQuery request, CancellationToken cancellationToken)
        {
            var list = await _dbContext.Donuts
                .OrderBy(x => x.Id)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new DonutListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync(cancellationToken);

            return Result<List<DonutListItem>>.Success(list);
        }

        public async Task<Result<DonutDTO>> Handle(FindQuery<int, DonutDTO> request, CancellationToken cancellationToken)
        {
            var donut = await _dbContext.Donuts
                .Where(x => x.Id == request.Key)
                .Select(x => new DonutDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (donut == null)
                return Result<DonutDTO>.Fail(HttpStatusCode.NotFound, "Dona no encontrada");

            return Result<DonutDTO>.Success(donut);
        }
    }
}
