namespace Catalog.API.Products.CreateProduct;

internal record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageUrl,
    decimal Price);

internal record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
                async (CreateProductRequest request,
                    ISender sender,
                    CancellationToken ct) =>
                {
                    CreateProductCommand command = request.Adapt<CreateProductCommand>();
                    CreateProductResult result = await sender.Send(command, ct);
                    CreateProductResponse response = result.Adapt<CreateProductResponse>();

                    return Results.Created($"/products/{result.Id}", response);
                }).WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create product")
            .WithDescription("Create product");
    }
}