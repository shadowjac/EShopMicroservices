using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

internal sealed class DeleteProductHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<DeleteProductHandler> _logger;

    public DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);
        
        _session.Delete<Product>(command.Id);
        
        await _session.SaveChangesAsync(cancellationToken);
        
        return new DeleteProductResult(true);
    }
}