using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Interceptors;

namespace Catalog;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<AuditLoggingInterceptor>();

        var connectionString = configuration.GetConnectionString("SqlConnection");

        services.AddDbContext<CatalogDbContext>((provider, options) =>
        {
            var interceptor = provider.GetService<AuditLoggingInterceptor>()
                              ?? throw new NullReferenceException(nameof(AuditLoggingInterceptor));

            options.EnableSensitiveDataLogging()
                .UseNpgsql(connectionString)
                .AddInterceptors(interceptor)
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IDataSeeder, CatalogSeeder>();
        return services;
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        app.UseMigrations<CatalogDbContext>();
        return app;
    }
}