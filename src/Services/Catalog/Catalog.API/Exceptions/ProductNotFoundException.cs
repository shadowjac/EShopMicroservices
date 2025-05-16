namespace Catalog.API.Exceptions;

public sealed class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Product not found.")
    {
    }

    public ProductNotFoundException(Guid id) : base($"Product with id {id} not found.")
    {
    }

    public ProductNotFoundException(string message) : base(message)
    {
    }

    public ProductNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}