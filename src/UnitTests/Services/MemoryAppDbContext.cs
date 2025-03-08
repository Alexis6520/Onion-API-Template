using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Services
{
    /// <summary>
    /// Implementación de servicio de base de datos en memoria
    /// </summary>
    /// <param name="databaseName">Nombre de la base de datos</param>
    public class MemoryAppDbContext(string databaseName) : AppDbContext
    {
        private readonly string _databaseName = databaseName;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_databaseName);
        }
    }
}
