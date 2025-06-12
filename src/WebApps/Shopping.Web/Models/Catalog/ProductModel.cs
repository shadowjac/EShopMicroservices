namespace Shopping.Web.Models.Catalog;

public sealed class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageFile { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<string> Category { get; set; } = [];
}

// Wrapper classes
public record GetProductResponse(IEnumerable<ProductModel> Products);
public record GetProductByIdResponse(ProductModel Product);
public record GetProductByCategoryResponse(IEnumerable<ProductModel> Products);