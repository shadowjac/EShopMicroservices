using Shopping.Web.Models.Basket;

namespace Shopping.Web.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{userName}")]
    Task<GetBasketResponse> GetBasketAsync(string userName, CancellationToken cancellationToken = default);
    
    [Post("/basket-service/basket")]
    Task<StoreBasketResponse> StoreBasketAsync(StoreBasketRequest request, CancellationToken cancellationToken = default);
    
    [Delete("/basket-service/basket/{userName}")]
    Task<DeleteBasketResponse> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default);
    
    [Post("/basket-service/basket/checkout")]
    Task<CheckoutBasketResponse> CheckoutBasketAsync(CheckoutBasketRequest request, CancellationToken cancellationToken = default);
}