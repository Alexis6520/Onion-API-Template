using Application.Abstractions;
using Application.Models;
using Application.Models.Donuts;
using Application.Queries.Donuts;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Application.Handlers.Donuts
{
    public class DonutQueryHandler(AppDbContext dbContext) :
        IRequestHandler<DonutsListQuery, List<DonutListItem>>
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
    }
}
