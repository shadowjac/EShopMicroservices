using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductResult(bool IsSuccess);

public record UpdateProductCommand(Guid Id,
    string Name,
    List<string> Category,
    string Description,
    decimal Price,
    string ImageFile) : ICommand<UpdateProductResult>;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
            .WithMessage("Product Id is required");
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must have between 2 and 150 characters");
        RuleFor(x => x.Price).GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}

internal sealed class UpdateProductHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<UpdateProductHandler> _logger;

    public UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateProductHandler.Handle: called with {@Command}", command);
        
        var product = await _session.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new ProductNotFoundException(command.Id);

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.Price = command.Price;
        product.ImageFile = command.ImageFile;
        
        _session.Update(product);
        await _session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(true);
    }
}