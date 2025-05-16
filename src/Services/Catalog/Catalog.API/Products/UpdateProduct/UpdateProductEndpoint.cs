namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(
    Guid Id,
    string Name,
    string Description,
    List<string> Category,
    decimal Price,
    string ImageFile);

public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            
            var result = await sender.Send(command, cancellationToken);
            
            var response = result.Adapt<UpdateProductResponse>();
            
            return Results.Ok(response);
        }).WithSummary("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}