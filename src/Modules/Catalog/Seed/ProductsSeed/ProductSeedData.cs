using Bogus;

namespace Catalog.Seed.ProductsSeed;

public static class ProductSeedData
{
    public static List<Product> GetProductsSeedData()
    {
        var productFaker = new Faker<Product>()
            .CustomInstantiator(f => Product.Create(
                Guid.NewGuid(),
                f.Commerce.ProductName(),
                f.Commerce.Categories(3).ToList(),
                f.Commerce.ProductDescription(),
                f.Internet.Url(),
                Convert.ToDecimal(f.Commerce.Price())
            ));
        
        return productFaker.Generate(100).ToList();
    }
}