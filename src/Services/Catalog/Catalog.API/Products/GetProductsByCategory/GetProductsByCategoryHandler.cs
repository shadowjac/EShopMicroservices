using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

internal class GetProductsByCategoryHandler  : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    private readonly IDocumentSession _session;

    public GetProductsByCategoryHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await _session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);
        
        return new GetProductByCategoryResult(products);
    }
}