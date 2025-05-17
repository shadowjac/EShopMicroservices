using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions;

internal sealed class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid id) 
        : base("Product", id)
    {
    }
}