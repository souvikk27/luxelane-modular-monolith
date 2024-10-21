using Catalog.Products.Models;

namespace Catalog.Products.Events;

public record ProductCreatedDomainEvent(Product product) : IDomainEvent;