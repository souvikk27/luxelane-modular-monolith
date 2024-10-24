using System.Linq.Expressions;

namespace Catalog.Products.Repository;

public class ProductRepository : RepositoryBase<Product, CatalogDbContext>, IProductRepository
{
    public ProductRepository(CatalogDbContext context) : base(context)
    {
    }

    public override Expression<Func<CatalogDbContext, DbSet<Product>>> DataSet() => o => o.Product;

    public override Expression<Func<Product, object>> Key() => o => o.Id;
}