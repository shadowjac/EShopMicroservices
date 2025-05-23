using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly DiscountContext _dbContext;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons
                         .FirstOrDefaultAsync(c => c.ProductName == request.ProductName, context.CancellationToken)
                     ?? new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

        _logger.LogInformation("Discount is retrieved for the product name: {ProductName}, Amount: {Amount}",
            coupon.ProductName,
            coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        if (request is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        }
        
        var coupon = request.Coupon.Adapt<Coupon>();
        
        await _dbContext.Coupons.AddAsync(coupon, context.CancellationToken);
        await _dbContext.SaveChangesAsync(context.CancellationToken);
        
        _logger.LogInformation("Discount is successfully created. Product Name: {ProductName}, Amount: {Amount}",
            coupon.ProductName,
            coupon.Amount);
        
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        if (request is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        }
        
        var coupon = request.Coupon.Adapt<Coupon>();
        
        _dbContext.Coupons.Update(coupon);
        await _dbContext.SaveChangesAsync(context.CancellationToken);

        _logger.LogInformation("Discount is successfully updated. Product Name: {ProductName}, Amount: {Amount}",
            coupon.ProductName,
            coupon.Amount);
        
        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons
            .FirstOrDefaultAsync(c => c.ProductName == request.ProductName, context.CancellationToken);

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} not found"));
        }
        
        _dbContext.Coupons.Remove(coupon);
        await _dbContext.SaveChangesAsync(context.CancellationToken);
        
        _logger.LogInformation("Discount is successfully deleted. Product Name: {ProductName}, Amount: {Amount}",
            coupon.ProductName,
            coupon.Amount);
        
        var response = new DeleteDiscountResponse
        {
            Success = true,
        };
        return response;
    }
}