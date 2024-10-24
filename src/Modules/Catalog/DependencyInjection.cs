using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstraction;
using Shared.Data.Interceptors;

namespace Catalog;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddMediatR(config => { config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        services.AddScoped<ISaveChangesInterceptor, AuditLoggingInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

        var connectionString = configuration.GetConnectionString("SqlConnection")
                               ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<CatalogDbContext>((provider, options) =>
        {
            var interceptor = provider.GetService<ISaveChangesInterceptor>()
                              ?? throw new NullReferenceException(nameof(ISaveChangesInterceptor));

            options.EnableSensitiveDataLogging()
                .UseNpgsql(connectionString)
                .AddInterceptors(interceptor)
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CatalogDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        services.AddScoped<IDataSeeder, CatalogSeeder>();
        return services;
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        app.UseMigrations<CatalogDbContext>();
        return app;
    }
}