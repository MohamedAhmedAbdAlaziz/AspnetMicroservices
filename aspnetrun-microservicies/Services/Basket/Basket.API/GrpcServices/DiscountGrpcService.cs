using Discount.Grpc.Protos;
using Grpc.Core;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoserviceClient)
        {
            _discountProtoService = discountProtoserviceClient;
        }
        public async Task<CouponModel> GetDiscount(string productName)
        {
            try
            {
                var discountRequest = new GetDiscountRequest { ProductName = productName };
                return await _discountProtoService.GetDiscountAsync(discountRequest);
            
            }
    catch (Exception ex)
    {
        // Log the exception details
    
        throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
    }

        }
    }
}