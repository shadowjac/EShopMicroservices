using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResult(Product Product);

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

internal sealed class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<GetProductByIdHandler> _logger;

    public GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductByIdHandler.Handle called with {@Query}", query);
        
        var product = await  _session.LoadAsync<Product>(query.Id, cancellationToken) 
                      ?? throw new ProductNotFoundException(query.Id);
        return new GetProductByIdResult(product);
    }
}