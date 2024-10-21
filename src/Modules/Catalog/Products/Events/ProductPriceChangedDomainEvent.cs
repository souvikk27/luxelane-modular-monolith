using Catalog.Products.Models;

namespace Catalog.Products.Events;

public record ProductPriceChangedDomainEvent(Product product) : IDomainEvent;