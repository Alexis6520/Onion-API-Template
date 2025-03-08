using Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTests.Services;

namespace UnitTests.Abstractions
{
    /// <summary>
    /// Clase base para todas las pruebas
    /// </summary>
    /// <typeparam name="THandler"></typeparam>
    public abstract class BaseTest<THandler>
    {
        private AppDbContext? _dbContext;
        private ILogger<THandler>? _logger;

        public AppDbContext DbContext => _dbContext ??= new MemoryAppDbContext(typeof(THandler).Name);
        public ILogger<THandler> Logger => _logger ??= new Mock<ILogger<THandler>>().Object;
    }
}
