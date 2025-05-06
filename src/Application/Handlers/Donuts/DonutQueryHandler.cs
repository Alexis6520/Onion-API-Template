using Application.Models.Donuts;
using Application.Queries;
using Application.Queries.Donuts;
using Application.RP;
using Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Handlers.Donuts
{
    public class DonutQueryHandler(AppDbContext dbContext) :
        IRequestHandler<DonutListQuery, List<DonutItem>>,
        IRequestHandler<FindQuery<int, DonutDTO>, Result<DonutDTO>>
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<List<DonutItem>> Handle(DonutListQuery request, CancellationToken cancellationToken)
        {
            List<DonutItem> donuts = await _dbContext.Donuts
                .Select(d => new DonutItem
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return donuts;
        }

        public async Task<Result<DonutDTO>> Handle(FindQuery<int, DonutDTO> request, CancellationToken cancellationToken)
        {
            DonutDTO? donut = await _dbContext.Donuts
                .Where(d => d.Id == request.Key)
                .Select(d => new DonutDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Price = d.Price
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (donut == null)
                return Result<DonutDTO>.Failure(HttpStatusCode.NotFound, Errors.NOT_FOUND);

            return Result<DonutDTO>.Success(donut);
        }
    }
}
