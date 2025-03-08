using Domain.Services;
using Infrastructure.EFCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;

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
