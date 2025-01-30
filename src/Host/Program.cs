using Application;
using Host.Middlewares;
using Infrastructure;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Reflection;

var logger = LogManager.GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services
        .AddApplicationServices()
        .AddInfrastructure();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Onion API Template",
            Description = "Plantilla de API Web con arquitectura cebolla"
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseAuthorization();

    app.UseMiddleware<NLogRequestPostedBodyMiddleware>
        (new NLogRequestPostedBodyMiddlewareOptions());

    app.MapControllers();

    app.Run();
}
catch (HostAbortedException) { }
catch (Exception ex)
{
    logger.Error(ex, "Programa detenido por excepción");
    throw;
}
finally
{
    LogManager.Shutdown();
}

public partial class Program { }