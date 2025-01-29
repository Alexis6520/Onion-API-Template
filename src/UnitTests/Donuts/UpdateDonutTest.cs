using Application.Commands.Donuts;
using Application.Handlers.Donuts;
using Domain.Entities;
using System.Net;
using UnitTests.Abstractions;

namespace UnitTests.Donuts
{
    /// <summary>
    /// Prueba de actualización de dona
    /// </summary>
    [TestClass]
    public class UpdateDonutTest : BaseTest<UpdateDonutHandler>
    {
        [TestMethod]
        public async Task HappyPath()
        {
            var donut = new Donut
            {
                Name = "Frambuesa",
                Description = "La mejor dona del krispy kreme",
                Price = 19.99m
            };

            DbContext.Donuts.Add(donut);
            await DbContext.SaveChangesAsync();
            var handler = new UpdateDonutHandler(DbContext, Logger);

            var command = new UpdateDonutCommand
            {
                Id = donut.Id,
                Name = donut.Name,
                Description = donut.Description,
                Price = 0,
            };

            var result = await handler.Handle(command, default);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            Assert.AreEqual(command.Price, donut.Price);
        }

        [TestMethod]
        public async Task NonExistingDonut()
        {
            var handler = new UpdateDonutHandler(DbContext, Logger);

            var command = new UpdateDonutCommand
            {
                Id = 3312,
                Name = "Glaseada original",
                Price = 0,
            };

            var result = await handler.Handle(command, default);
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
