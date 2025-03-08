using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Services;

namespace Infrastructure.EFCore.SqlServer
{
    /// <summary>
    /// Implementación de AppDbContext con Microsoft SQL Server
    /// </summary>
    /// <param name="configuration"></param>
    public class SqlServerAppDbContext(IConfiguration configuration) : AppDbContext
    {
        private readonly IConfiguration _configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                _configuration.GetConnectionString("Default"));
        }
    }
}
