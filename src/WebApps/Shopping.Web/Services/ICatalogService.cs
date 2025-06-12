public interface ICatalogService
{
    [Get("/catalog-service/products?pageNumber={pageNumber}&pageSize={pageSize}")]
    Task<GetProductResponse> GetProductsAsync(int? pageNumber = 1, int? pageSize = 10, CancellationToken cancellationToken = default);
    
    [Get("/catalog-service/products/{productId}")]
    Task<GetProductByIdResponse> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken = default);
    
    [Get("/catalog-service/products/category{category}")]
    Task<GetProductByCategoryResponse> GetProductsByCategoryAsync(string category, CancellationToken cancellationToken = default);
}