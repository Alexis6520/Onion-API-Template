using Application.Commands.Donuts;
using Application.Models.Donuts;
using Application.RP;
using Domain.Entities;
using IntegrationTests.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace IntegrationTests
{
    public class DonutTest(CustomWebAppFactory factory) : BaseTest(factory)
    {
        private const string BaseUrl = "/api/donuts";
        private readonly List<int> _idsToDelete = [];

        [Fact]
        public async Task Create()
        {
            var command = new CreateDonutCommand
            {
                Name = "Test",
                Description = "Test",
                Price = 1.0m
            };

            HttpResponseMessage response = await Client.PostAsJsonAsync(BaseUrl, command);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Result<int>>();
            Assert.NotNull(result);

            bool exists = await DbContext.Donuts
                .AnyAsync(x => x.Id == result.Value);

            Assert.True(exists, "La donita no fue creada en la base de datos.");
            _idsToDelete.Add(result.Value);
        }

        [Fact]
        public async Task GetList()
        {
            List<Donut> donuts = [];

            for (int i = 0; i < 3; i++)
            {
                donuts.Add(new Donut
                {
                    Name = $"ListTest {i}",
                    Description = $"ListTest {i}",
                    Price = 1.0m
                });
            }

            DbContext.Donuts.AddRange(donuts);
            await DbContext.SaveChangesAsync();
            _idsToDelete.AddRange(donuts.Select(x => x.Id));
            HttpResponseMessage response = await Client.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Result<List<DonutItem>>>();
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.True(result.Value.Count >= donuts.Count, "No se obtuvieron todas las donitas de la base de datos.");
        }

        [Fact]
        public async Task GetById()
        {
            var donut = new Donut
            {
                Name = "GetByIdTest",
                Description = "GetByIdTest",
                Price = 1.0m
            };

            DbContext.Donuts.Add(donut);
            await DbContext.SaveChangesAsync();
            _idsToDelete.Add(donut.Id);
            HttpResponseMessage response = await Client.GetAsync($"{BaseUrl}/{donut.Id}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Result<DonutDTO>>();
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
        }

        protected override void Cleanup()
        {
            var query = DbContext.Donuts
                .Where(x => _idsToDelete.Contains(x.Id));

            DbContext.RemoveRange(query);
            DbContext.SaveChanges();
        }
    }
}
