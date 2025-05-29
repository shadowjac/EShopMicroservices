namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderResult(Guid Id);

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName)
            .NotEmpty()
            .WithMessage("Order name is required.");

        RuleFor(x => x.Order.CustomerId)
            .NotNull()
            .WithMessage("Customer ID is required.");

        RuleFor(x => x.Order.OrderItems)
            .NotEmpty()
            .WithMessage("Order items cannot be empty.");
    }
}