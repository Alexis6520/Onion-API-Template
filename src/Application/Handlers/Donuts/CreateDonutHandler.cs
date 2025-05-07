using Application.Commands.Donuts;
using Application.RP;
using Domain.Entities;
using Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Handlers.Donuts
{
    public class CreateDonutHandler(AppDbContext dbContext, ILogger<CreateDonutHandler> logger) : IRequestHandler<CreateDonutCommand, Result<int>>
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly ILogger<CreateDonutHandler> _logger = logger;

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
            _logger.LogInformation("Dona creada con ID: {Id}", donut.Id);
            return Result<int>.Success(donut.Id, HttpStatusCode.Created);
        }
    }
}
