using Ardalis.Result;
using Ardalis.SharedKernel;
using ORH.Application.Interface.Customer;

namespace ORH.Application.UseCase.Customer.Commands
{
    public record PurchaseProductCommand(PurchaseProductRequest Request) : ICommand<Result<PurchaseProductResponse>>;

    public class PurchaseProductHandler(IPurchaseProduct service) : ICommandHandler<PurchaseProductCommand, Result<PurchaseProductResponse>>
    {
        public async Task<Result<PurchaseProductResponse>> Handle(PurchaseProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var isProductInStock = await service.IsProductInStockAsync(request.Request);

                if (!isProductInStock)
                {
                    return Result.Invalid(new ValidationError()
                    {
                        Identifier = nameof(request.Request.Quantity),
                        ErrorMessage = "Not enough product in stock."
                    });
                }

                var result = await service.ExecutePurchaseProductAsync(request.Request);

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }

    public class PurchaseProductRequest
    {
        public required int CustomerId { get; set; }
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }

    public class PurchaseProductResponse
    {
        public required DateTime PurchasedDate { get; set; }
    }
}
