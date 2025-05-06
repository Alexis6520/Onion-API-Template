using Application.Models.Donuts;
using Application.Queries.Donuts;
using Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Donuts
{
    public class DonutQueryHandler(AppDbContext dbContext) : IRequestHandler<DonutListQuery, List<DonutItem>>
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
    }
}
