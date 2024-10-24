using Shared.Abstraction;

namespace Catalog.Data;

public class CatalogDbContext : DbContext, IUnitOfWork
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Product => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("catalog");
        builder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new InvalidOperationException("Concurrency error", ex);
        }
    }
}