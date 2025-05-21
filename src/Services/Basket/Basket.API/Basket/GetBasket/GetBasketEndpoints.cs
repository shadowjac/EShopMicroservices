namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}",
            async (string userName,
                ISender sender,
                CancellationToken ct) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName), ct);
                
                var response = result.Adapt<GetBasketResponse>();
                
                return Results.Ok(response);
            }).WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket")
            .WithDescription("Get Basket");
        
    }
}