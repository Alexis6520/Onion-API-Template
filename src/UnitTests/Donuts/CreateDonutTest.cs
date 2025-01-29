using Application.Commands.Donuts;
using Application.Handlers.Donuts;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UnitTests.Abstractions;

namespace UnitTests.Donuts
{
    /// <summary>
    /// Pruebas de creación de dona
    /// </summary>
    [TestClass]
    public class CreateDonutTest : BaseTest<CreateDonutHandler>
    {
        [TestMethod]
        public async Task HappyPath()
        {
            var handler = new CreateDonutHandler(DbContext, Logger);

            var command = new CreateDonutCommand
            {
                Name = "Frambuesa",
                Description = "La mejor dona de Krispy Kreme",
                Price = 19.99m
            };

            var result = await handler.Handle(command, default);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            var exists = await DbContext.Donuts.AnyAsync(x => x.Id == result.Value);
            Assert.IsTrue(exists);
        }
    }
}
