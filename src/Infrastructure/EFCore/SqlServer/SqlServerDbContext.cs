using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.EFCore.SqlServer
{
    /// <summary>
    /// Implementación de AppDbContext para SQL Server.
    /// </summary>
    public class SqlServerDbContext(IConfiguration configuration) : AppDbContext
    {
        private readonly IConfiguration _configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
        }
    }
}
