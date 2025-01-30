using Application.Commands.Donuts;
using Application.Models;
using Application.Models.Donuts;
using IntegrationTests.Abstractions;
using IntegrationTests.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace IntegrationTests
{
    public class DonutTests(CustomWebAppFactory factory) : BaseTest(factory)
    {
        private const string BaseUrl = "api/donuts";
        private readonly List<int> _toDeleteIds = [];

        [Fact]
        public async Task Create()
        {
            var command = new CreateDonutCommand
            {
                Name = "Frambuesa",
                Description = "Dona de frambuesa",
                Price = 19.99m
            };

            var id = await CreateDonutAsync(command);

            var exists = await DbContext.Donuts
                .AnyAsync(x => x.Id == id);

            Assert.True(exists);
        }

        private async Task<int> CreateDonutAsync(CreateDonutCommand command)
        {
            using var response = await Client.PostAsJsonAsync(BaseUrl, command);
            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<Result<int>>();

            _toDeleteIds.Add(result.Value);
            return result.Value;
        }

        [Fact]
        public async Task GetList()
        {
            var command = new CreateDonutCommand
            {
                Name = "Frambuesa",
                Description = "Dona de frambuesa",
                Price = 19.99m
            };

            for (int i = 0; i < 3; i++)
            {
                await CreateDonutAsync(command);
            }

            var result = await Client.GetFromJsonAsync<Result<List<DonutListItem>>>(BaseUrl);
            Assert.True(result.Value.Count >= 3);
        }

        [Fact]
        public async Task GetById()
        {
            var command = new CreateDonutCommand
            {
                Name = "Frambuesa",
                Description = "Dona de frambuesa",
                Price = 19.99m
            };

            var id = await CreateDonutAsync(command);
            var result = await Client.GetFromJsonAsync<Result<DonutDTO>>($"{BaseUrl}/{id}");
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task Update()
        {
            var createCommand = new CreateDonutCommand
            {
                Name = "Frambuesa",
                Description = "Dona de frambuesa",
                Price = 19.99m
            };

            var id = await CreateDonutAsync(createCommand);

            var updateCommand = new UpdateDonutCommand
            {
                Name = "Glaseada original",
                Description = createCommand.Description,
                Price = createCommand.Price,
            };

            using var response = await Client.PutAsJsonAsync($"{BaseUrl}/{id}", updateCommand);
            response.EnsureSuccessStatusCode();

            var donut = await DbContext.Donuts
                .FindAsync([id]);

            Assert.Equal(donut.Name, updateCommand.Name);
            Assert.NotEqual(donut.Name, createCommand.Name);
        }

        [Fact]
        public async Task Delete()
        {
            var createCommand = new CreateDonutCommand
            {
                Name = "Frambuesa",
                Description = "Dona de frambuesa",
                Price = 19.99m
            };

            var id = await CreateDonutAsync(createCommand);
            using var response = await Client.DeleteAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var exists = await DbContext.Donuts.AnyAsync(x => x.Id == id);
            Assert.False(exists);
        }

        protected override void Cleanup()
        {
            DbContext.Donuts
                .RemoveRange(
                    DbContext.Donuts
                        .Where(x => _toDeleteIds.Contains(x.Id)));

            DbContext.SaveChanges();
        }
    }
}