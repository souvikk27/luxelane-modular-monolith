using Shared;

namespace Catalog.Products.Features.Commands;

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageUrl,
    decimal Price) : ICommand;