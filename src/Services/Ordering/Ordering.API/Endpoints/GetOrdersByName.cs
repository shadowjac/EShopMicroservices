using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints;

public record GetOrdersByNameRequest(string Name);

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{name}",
                async (string name,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetOrdersByNameQuery(name), 
                        cancellationToken);

                    var response = result.Adapt<GetOrdersByNameResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetOrdersByName")
            .WithTags("Orders")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get orders by name")
            .WithDescription("Retrieves a list of orders filtered by the specified name.");
    }
}