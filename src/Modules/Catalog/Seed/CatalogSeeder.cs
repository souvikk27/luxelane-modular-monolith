using Catalog.Seed.ProductsSeed;

namespace Catalog.Seed;

public class CatalogSeeder : IDataSeeder
{
    private readonly CatalogDbContext _catalogContext;

    public CatalogSeeder(CatalogDbContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task SeedAllAsync()
    {
        if (!await _catalogContext.Product.AnyAsync())
        {
            await _catalogContext.Product.AddRangeAsync(ProductSeedData.GetProductsSeedData());
            await _catalogContext.SaveChangesAsync();
        }
    }
}