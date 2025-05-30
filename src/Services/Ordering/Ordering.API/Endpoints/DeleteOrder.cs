using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

public record DeleteOrderRequest(Guid Id);

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id:guid}", async (Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new DeleteOrderCommand(id), cancellationToken);

                var response = result.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            }).WithName("DeleteOrder")
            .WithSummary("Delete an order by ID")
            .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("Orders")
            .WithSummary("Delete an order by ID")
            .WithDescription("Deletes an order from the system by its ID.");
    }
}