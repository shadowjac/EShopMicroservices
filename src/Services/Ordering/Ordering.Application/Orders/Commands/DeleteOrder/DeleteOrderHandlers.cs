﻿using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandlers : IRequestHandler<DeleteOrderCommand, DeleteOrderResult>
{
    private readonly IApplicationDbContext _context;

    public DeleteOrderHandlers(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderId);
        var order = await _context.Orders
            .FindAsync([orderId], cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }
}