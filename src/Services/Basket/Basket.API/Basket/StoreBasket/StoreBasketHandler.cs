using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public sealed class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart)
            .NotNull()
            .WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName)
            .NotNull()
            .WithMessage("Username is required");
    }
}

public sealed class StoreBasketHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult> 
{
    private readonly IBasketRepository _repository;
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;
    private readonly ILogger<StoreBasketHandler> _logger;

    public StoreBasketHandler(IBasketRepository repository,
        DiscountProtoService.DiscountProtoServiceClient discountProtoService,
        ILogger<StoreBasketHandler> logger)
    {
        _repository = repository;
        _discountProtoService = discountProtoService;
        _logger = logger;
    }
    
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await CalculateDiscountAsync(command.Cart, cancellationToken);
        await _repository.StoreBasketAsync(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task CalculateDiscountAsync(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await _discountProtoService.GetDiscountAsync(new GetDiscountRequest
            {
                ProductName = item.ProductName
            }, cancellationToken: cancellationToken);
            
            _logger.LogInformation("Discount is {DiscountAmount} for the product with name {ProductName}",
                coupon.Amount, coupon.ProductName);
            
            item.Price -= coupon.Amount;
        }
    }
}