﻿using Catalog.API.Models;

namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetProductsResponse(IEnumerable<Product> Products);

public sealed class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
                async ([AsParameters] GetProductsRequest request,
                    ISender sender,
                    CancellationToken ct) =>
                {
                    var query = request.Adapt<GetProductsQuery>();
                    var result = await sender.Send(query, ct);

                    var response = result.Adapt<GetProductsResponse>();

                    return Results.Ok(response);
                }).WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}