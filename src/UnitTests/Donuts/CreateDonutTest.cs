using Application.Commands.Donuts;
using Application.Handlers.Donuts;
using Application.RP;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace UnitTests.Donuts
{
    [TestClass]
    public class CreateDonutTest : BaseTest<CreateDonutHandler>
    {
        [TestMethod]
        public async Task HappyPath()
        {
            var command = new CreateDonutCommand
            {
                Name = "Test",
                Description = "Test",
                Price = 19.99m
            };

            var handler = new CreateDonutHandler(DbContext, Logger);
            Result<int> result = await handler.Handle(command, default);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

            var saved = await DbContext.Donuts
                .AnyAsync(x => x.Id == result.Value);

            Assert.IsTrue(saved);
        }

        [TestMethod]
        public async Task UnavailableName()
        {
            var donut = new Donut
            {
                Name = "Unavailable",
                Description = "Test",
                Price = 19.99m
            };

            DbContext.Donuts.Add(donut);
            await DbContext.SaveChangesAsync();

            var command = new CreateDonutCommand
            {
                Name = donut.Name,
                Description = "Test",
                Price = 19.99m
            };

            var handler = new CreateDonutHandler(DbContext, Logger);
            Result<int> result = await handler.Handle(command, default);
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode);
            Error? error = result.Errors?.FirstOrDefault();
            _ = error ?? throw new Exception("Error es null");
            Assert.AreEqual(Errors.DONUT_NAME_NOT_AVAILABLE.Code, error.Code);
        }
    }
}
