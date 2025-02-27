﻿using Domain.Entities;
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
            // Configuramos las entidades para la implementación actual de este servicio
            var contextType = GetType();
            var configNamespace = $"{contextType.Namespace}.Configuration";

            modelBuilder.ApplyConfigurationsFromAssembly(
                contextType.Assembly,
                configType => configType.Namespace == configNamespace);
        }
    }
}
