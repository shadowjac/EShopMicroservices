namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        
    }
}

public sealed class DeleteBasketCommandHandler  : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    private readonly IBasketRepository _repository;

    public DeleteBasketCommandHandler(IBasketRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await _repository.DeleteBasketAsync(command.UserName, cancellationToken);
        return new DeleteBasketResult(true);
    }
}