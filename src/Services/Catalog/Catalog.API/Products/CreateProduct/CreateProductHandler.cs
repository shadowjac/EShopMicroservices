using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;

internal record CreateProductCommand(string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
    ) : ICommand<CreateProductResult>;

internal record CreateProductResult(Guid Id);

internal sealed class CreateProductHandler :  ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly ILogger<CreateProductHandler> _logger;

    public CreateProductHandler(ILogger<CreateProductHandler> logger)
    {
        _logger = logger;
    }

    public Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating product {Name}", command.Name);
        Product product = new()
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        
        // 2. save to database
        // 3. return result
        
        return Task.FromResult(new CreateProductResult(product.Id));
    }
}
