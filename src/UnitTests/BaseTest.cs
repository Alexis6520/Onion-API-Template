using Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTests.Services;

namespace UnitTests
{
    public abstract class BaseTest<THandler> : IDisposable
        where THandler : class
    {
        private readonly Mock<ILogger<THandler>> _loggerMock = new();
        private readonly AppDbContext _dbContext = new MemoryDbContext(typeof(THandler).Name);

        protected ILogger<THandler> Logger => _loggerMock.Object;
        protected AppDbContext DbContext => _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
