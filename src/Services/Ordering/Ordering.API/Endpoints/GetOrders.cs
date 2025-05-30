using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

public record GetOrdersRequest(PaginationRequest PaginationRequest);

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders",
                async ([AsParameters]PaginationRequest request,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetOrdersQuery(request), 
                        cancellationToken);

                    var response = result.Adapt<GetOrdersResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetOrders")
            .WithTags("Orders")
            .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get orders")
            .WithDescription("Retrieves a paginated list of orders.");
    }
}