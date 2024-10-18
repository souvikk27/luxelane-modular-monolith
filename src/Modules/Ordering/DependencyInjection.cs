using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderingMoodule(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}