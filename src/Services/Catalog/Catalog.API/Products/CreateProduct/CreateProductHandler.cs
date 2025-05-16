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
    private readonly IDocumentSession _session;
    private readonly ILogger<CreateProductHandler> _logger;

    public CreateProductHandler(IDocumentSession session, ILogger<CreateProductHandler> logger)
    {
        _logger = logger;
        _session = session;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
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
        
        _session.Store(product);
        await _session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(product.Id);
    }
}
