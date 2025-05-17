using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResult(Product Product);

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

internal sealed class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly IDocumentSession _session;
    public GetProductByIdHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await  _session.LoadAsync<Product>(query.Id, cancellationToken) 
                      ?? throw new ProductNotFoundException(query.Id);
        return new GetProductByIdResult(product);
    }
}