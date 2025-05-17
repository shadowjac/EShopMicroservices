using Catalog.API.Models;
using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync(token: cancellation))
        {
            return;
        }

        // Marten UPSERT will cater for existing records 
        session.Store(GetProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetProducts() =>
    [
        // generate 10 products
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Laptop",
            Description = "High performance laptop",
            Price = 1200.00m,
            Category =
                ["Electronics"]
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Smartphone",
            Description = "Latest model smartphone",
            Price = 800.00m,
            Category =
                ["Electronics"]
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Headphones",
            Description = "Noise-cancelling headphones",
            Price = 150.00m,
            Category =
                ["Audio"]
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Coffee Maker",
            Description = "Automatic coffee maker",
            Price = 90.00m,
            Category =
                ["Home Appliances"]
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Gaming Mouse",
            Description = "Ergonomic gaming mouse",
            Price = 60.00m,
            Category =
                ["Accessories"]
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Backpack",
            Description = "Water-resistant backpack",
            Price = 45.00m,
            Category =
                ["Travel"]
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Desk Lamp",
            Description = "LED desk lamp",
            Price = 35.00m,
            Category =
                ["Office"]
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Bluetooth Speaker",
            Description = "Portable Bluetooth speaker",
            Price = 70.00m,
            Category =
                ["Audio"]
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Fitness Tracker",
            Description = "Wearable fitness tracker",
            Price = 110.00m,
            Category =
                ["Wearables"]
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "E-book Reader",
            Description = "Lightweight e-book reader",
            Price = 130.00m,
            Category =
                ["Electronics"]
        },
    ];
}