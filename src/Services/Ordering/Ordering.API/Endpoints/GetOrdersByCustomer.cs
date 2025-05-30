using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerRequest(string CustomerId);

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId:guid}",
                async (Guid customerId,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetOrdersByCustomerQuery(customerId), 
                        cancellationToken);

                    var response = result.Adapt<GetOrdersByCustomerResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetOrdersByCustomer")
            .WithTags("Orders")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get orders by customer")
            .WithDescription("Retrieves a list of orders for a specific customer.");
    }
}