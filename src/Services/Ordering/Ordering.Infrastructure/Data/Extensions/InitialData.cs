namespace Ordering.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Customer> Customers =>
    [
        Customer.Create(CustomerId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D0")), "John Doe", "johndoe@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D1")), "Jane Smith", "jane@gmail.com")
    ];
    
    public static IEnumerable<Product> Products =>
    [
        Product.Create(ProductId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D2")), "Laptop", 1200.00m),
        Product.Create(ProductId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D3")), "Smartphone",  800.00m),
        Product.Create(ProductId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D4")), "Headphones",  200.00m)
    ];
    
    public static IEnumerable<Order> OrdersWithItems 
    {
        get
        {
            var address1 = Address.Of("john", "doe", "johndoe@gmail.com","fake st","USA", "Springfield", "32704");
            var address2 = Address.Of("jane", "smith", "jane@gmail.com", "fake st", "USA", "Miami", "32705");
            
            var payment1 = Payment.Of("john", "1234567890123456", "12/25", "123", 1);
            var payment2 = Payment.Of("jane", "1234567890123456", "12/25", "123", 2);
            
            var order1 = Order.Create(
                OrderId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D5")),
                CustomerId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D0")),
                OrderName.Of("ORD_1"), 
                shippingAddress: address1,
                billingAddress: address2,
                payment1
            );
            
            order1.AddItem(ProductId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D2")), 500, 2);
            order1.AddItem(ProductId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D3")), 200, 1);
            
            var order2 = Order.Create(
                OrderId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D6")),
                CustomerId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D1")),
                OrderName.Of("ORD_2"), 
                shippingAddress: address2,
                billingAddress: address1,
                payment2
            );
            order2.AddItem(ProductId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D4")), 200, 1);
            order2.AddItem(ProductId.Of(new Guid("71A732BD-0855-4F10-9D6E-1851CFE6E4D2")), 1200, 1);
            
            return [order1, order2];
        }
    }
}