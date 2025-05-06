using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    /// <summary>
    /// Servicio de base de datos
    /// </summary>
    public abstract class AppDbContext : DbContext
    {
        public DbSet<Donut> Donuts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Type contextType = GetType();
            string configNamespace = $"{contextType.Namespace}.Configuration";

            modelBuilder.ApplyConfigurationsFromAssembly(
                contextType.Assembly,
                t => t.Namespace == configNamespace
            );
        }
    }
}
