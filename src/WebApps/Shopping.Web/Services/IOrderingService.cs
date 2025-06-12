namespace Shopping.Web.Services;

public interface IOrderingService
{
    [Get("/ordering-service/orders?pageIndex={pageIndex}&pageSize={pageSize}")]
    Task<GetOrdersResponse> GetOrdersAsync(string userName, int pageIndex = 1, int pageSize = 10,
        CancellationToken cancellationToken = default);
    
    [Get("/ordering-service/orders/{orderName}")]
    Task<GetOrderByNameResponse> GetOrderByNameAsync(string orderName, CancellationToken cancellationToken = default);
    
    [Get("/ordering-service/orders/customer/{customerId}")]
    Task<GetOrderByCustomerResponse> GetOrderByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
}