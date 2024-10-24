using Catalog.Products.Repository;
using Shared.Abstraction;

namespace Catalog.Products.Features.Commands;

internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            Guid.NewGuid(),
            request.Name,
            request.Category,
            request.Description,
            request.ImageUrl,
            request.Price);

        var result = await _productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success("Product created successfully");
    }
}