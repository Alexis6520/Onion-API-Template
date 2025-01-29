using Application.Commands;
using Application.Handlers.Donuts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UnitTests.Abstractions;

namespace UnitTests.Donuts
{
    /// <summary>
    /// Prueba de eliminación de dona
    /// </summary>
    [TestClass]
    public class DeleteDonutTest : BaseTest<DeleteDonutHandler>
    {
        [TestMethod]
        public async Task HappyPath()
        {
            var donut = new Donut
            {
                Name = "Frambuesa"
            };

            DbContext.Donuts.Add(donut);
            await DbContext.SaveChangesAsync();
            var handler = new DeleteDonutHandler(DbContext, Logger);
            var command = new DeleteCommand<int, Donut>(donut.Id);
            var result = await handler.Handle(command, default);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            var exists = await DbContext.Donuts.AnyAsync(x => x.Id == donut.Id);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public async Task NonExistingDonut()
        {
            var handler = new DeleteDonutHandler(DbContext, Logger);
            var command = new DeleteCommand<int, Donut>(3312);
            var result = await handler.Handle(command, default);
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
