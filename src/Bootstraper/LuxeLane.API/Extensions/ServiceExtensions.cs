using Basket;
using Catalog;
using Ordering;

namespace LuxeLane.API.Extensions;

public static class ServiceExtensions
{
    public static void RegisterDecoupledModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBasketModule(configuration)
            .AddCatalogModule(configuration)
            .AddOrderingMoodule(configuration);
    }
}