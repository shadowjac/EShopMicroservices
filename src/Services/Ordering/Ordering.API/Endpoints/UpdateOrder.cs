﻿using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);

public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders",
                async (UpdateOrderRequest request,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var command = request.Adapt<UpdateOrderCommand>();
                    
                    var result = await sender.Send(command, cancellationToken);
                    
                    var response = result.Adapt<UpdateOrderResponse>();
                    
                    return Results.Ok(response);
                })
            .WithName("UpdateOrder")
            .WithTags("Orders")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Updates an existing order")
            .WithDescription("Updates an existing order in the system. The updated order details must be provided in the request body.");
    }
}