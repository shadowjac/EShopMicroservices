namespace Shopping.Web.Models.Basket;

public class ShoppingCartModel
{
   public string UserName { get; set; } = string.Empty;
    public List<ShoppingCartItemModel> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}

public class ShoppingCartItemModel
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Color { get; set; } = string.Empty;
}

// Wrapper classes
public record GetBasketResponse(ShoppingCartModel Cart);
public record StoreBasketRequest(ShoppingCartModel Cart);
public record StoreBasketResponse(string UserName);
public record DeleteBasketResponse(bool IsSuccess);
