using Application.Abstractions;
using Application.Commands.Donuts;
using Application.Models;
using Services;
using System.Net;

namespace Application.Handlers.Donuts
{
    public class UpdateDonutHandler(AppDbContext dbContext) : IRequestHandler<UpdateDonutCommand>
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Result> Handle(UpdateDonutCommand request, CancellationToken cancellationToken)
        {
            var donut = await _dbContext.Donuts
                .FindAsync([request.Id], cancellationToken);

            if (donut == null)
                return Result.Fail(HttpStatusCode.NotFound, "Dona no encontrada");

            donut.Name = request.Name;
            donut.Description = request.Description;
            donut.Price = request.Price;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
