using Infrastructure.EFCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Domain.Services;

namespace Infrastructure
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext, SqlServerAppDbContext>();
            return services;
        }
    }
}
