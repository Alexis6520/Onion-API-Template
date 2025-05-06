using Application.Commands.Donuts;
using Application.RP;
using Domain.Entities;
using Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Handlers
{
    public class CreateDonutHandler(AppDbContext dbContext) : IRequestHandler<CreateDonutCommand, Result<int>>
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Result<int>> Handle(CreateDonutCommand request, CancellationToken cancellationToken)
        {
            bool nameAvailable = !await _dbContext.Donuts
                .AnyAsync(d => d.Name == request.Name, default);

            if (!nameAvailable)
            {
                return Result<int>.Failure(HttpStatusCode.Conflict, Errors.DONUT_NAME_NOT_AVAILABLE);
            }

            var donut = new Donut
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            _dbContext.Donuts.Add(donut);
            await _dbContext.SaveChangesAsync(default);
            return Result<int>.Success(donut.Id, HttpStatusCode.Created);
        }
    }
}
