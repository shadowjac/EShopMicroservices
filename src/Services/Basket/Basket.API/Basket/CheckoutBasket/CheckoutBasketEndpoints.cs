namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async
            (CheckoutBasketRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<CheckoutBasketCommand>();

                var result = await sender.Send(command, cancellationToken);

                var response = result.Adapt<CheckoutBasketResponse>();

                return Results.Ok(response);
            }).WithName("CheckoutBasket")
            .WithTags("Basket")
            .Produces<CheckoutBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Checkout Basket")
            .WithDescription("Checkout the basket for a user");
    }
}