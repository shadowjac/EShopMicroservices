using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.GetProducts;

public record GetProductsResult(IEnumerable<Product> Products);

public record GetProductsQuery : IQuery<GetProductsResult>;

internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsResult> 
{
    private readonly IDocumentSession _session;
    private readonly ILogger<GetProductsQueryHandler> _logger;

    public GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler>  logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductsQueryHandlerHandle: called with {@Query}", query);

        var products = await _session.Query<Product>().ToListAsync(cancellationToken);
        
        return new GetProductsResult(products);
    }
}