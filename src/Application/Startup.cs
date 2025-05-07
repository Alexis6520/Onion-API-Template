using Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Startup
    {
        /// <summary>
        /// Agrega los servicios de la aplicación al contenedor de inyección de dependencias.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            var assembly = typeof(Startup).Assembly;

            services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssembly(assembly);
                c.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}
