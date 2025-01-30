using IntegrationTests.Services;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace IntegrationTests.Abstractions
{
    /// <summary>
    /// Clase base para todas las pruebas
    /// </summary>
    /// <param name="factory">Fabrica de aplicación web personalizada</param>
    public abstract class BaseTest(CustomWebAppFactory factory) : IClassFixture<CustomWebAppFactory>, IDisposable
    {
        protected readonly CustomWebAppFactory Factory = factory;
        private HttpClient _client;
        private IServiceScope _scope;
        private AppDbContext _dbContext;

        public HttpClient Client => _client ??= Factory.CreateClient();

        public AppDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _scope ??= Factory.Services.CreateScope();

                    _dbContext = _scope.ServiceProvider
                        .GetRequiredService<AppDbContext>();
                }

                return _dbContext;
            }
        }

        /// <summary>
        /// Sobreescribe este método para limpiar después de cada método de prueba
        /// </summary>
        protected virtual void Cleanup()
        {
        }

        public void Dispose()
        {
            Cleanup();
            _client?.Dispose();
            _client = null;
            _dbContext?.Dispose();
            _dbContext = null;
            _scope?.Dispose();
            _scope = null;
            GC.SuppressFinalize(this);
        }
    }
}
