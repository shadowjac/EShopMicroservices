namespace Basket.API.Models;

public sealed class ShoppingCart
{
    public ShoppingCart()
    {
        
    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; } = null!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}
