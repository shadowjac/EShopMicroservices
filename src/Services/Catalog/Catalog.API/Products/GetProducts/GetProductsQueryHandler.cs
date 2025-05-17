﻿using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductsResult(IEnumerable<Product> Products);

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) 
    : IQuery<GetProductsResult>;

internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsResult> 
{
    private readonly IDocumentSession _session;
    public GetProductsQueryHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await _session
            .Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, 
                query.PageSize ?? 10,
                cancellationToken);
        
        return new GetProductsResult(products);
    }
}