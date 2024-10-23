using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Data;

public static class Extensions
{
    public static IApplicationBuilder UseMigrations<TContext>(this IApplicationBuilder app)
        where TContext : DbContext
    {
        InitializeDatabase<TContext>(app.ApplicationServices).GetAwaiter().GetResult();
        return app;
    }

    private static async Task InitializeDatabase<TContext>(IServiceProvider serviceProvider)
        where TContext : DbContext
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
        await dbContext.Database.MigrateAsync();
    }
}