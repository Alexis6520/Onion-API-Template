using Domain.Services;
using IntegrationTests.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public abstract class BaseTest(CustomWebAppFactory factory) :
        IClassFixture<CustomWebAppFactory>,
        IDisposable
    {
        protected CustomWebAppFactory Factory => factory;
        private readonly IServiceScope _scope = factory.Services.CreateScope();
        private AppDbContext? _dbContext;

        private readonly HttpClient _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        protected HttpClient Client => _client;
        protected AppDbContext DbContext => _dbContext ??= _scope.ServiceProvider.GetRequiredService<AppDbContext>();

        protected virtual void Cleanup() { }

        public void Dispose()
        {
            Cleanup();
            _dbContext?.Dispose();
            _scope.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}