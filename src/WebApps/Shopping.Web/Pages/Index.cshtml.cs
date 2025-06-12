using Shopping.Web.Models.Basket;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly IBasketService _basketService;
    private readonly ILogger<IndexModel> _logger;
    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    public IndexModel(ICatalogService catalogService,
        IBasketService basketService,
        ILogger<IndexModel> logger)
    {
        _catalogService = catalogService;
        _basketService = basketService;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Index page visited.");
        var result = await _catalogService.GetProductsAsync(cancellationToken: cancellationToken);
        ProductList = result.Products;
        return Page();
    }
    
    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding product {ProductId} to cart.", productId);
        
        var productResponse = await _catalogService.GetProductByIdAsync(productId, cancellationToken);

        var basket = await LoadUserBasketAsync(cancellationToken);
        
        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Quantity = 1,
            Price = productResponse.Product.Price,
            Color = "Black"
        });
        
        await _basketService.StoreBasketAsync(new StoreBasketRequest(basket), cancellationToken);
        
        return RedirectToPage("Cart");
    }

    private async Task<ShoppingCartModel> LoadUserBasketAsync(CancellationToken cancellationToken = default)
    {
        var userName = "jac";
        ShoppingCartModel basket;
        try
        {
           var basketResponse = await _basketService.GetBasketAsync(userName, cancellationToken);
            basket = basketResponse.Cart;
        }
        catch (ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogError(ex, "Error loading basket for user {UserName}", userName);
            basket = new ShoppingCartModel { UserName = userName };
        }
        
        return basket;
    }
}