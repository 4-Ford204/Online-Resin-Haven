using ORH.Application.UseCase.Customer.Commands;

namespace ORH.Application.Interface.Customer
{
    public interface IPurchaseProduct
    {
        Task<bool> IsProductInStockAsync(PurchaseProductRequest request);
        Task<PurchaseProductResponse> ExecutePurchaseProductAsync(PurchaseProductRequest request);
    }
}
