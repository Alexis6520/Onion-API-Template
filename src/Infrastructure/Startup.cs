using Domain.Services;
using Infrastructure.EFCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Startup
    {
        /// <summary>
        /// Agrega los servicios de infraestructura al contenedor de servicios.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext, SqlServerDbContext>();
            return services;
        }
    }
}
