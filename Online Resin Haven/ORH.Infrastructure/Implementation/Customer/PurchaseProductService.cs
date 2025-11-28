using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORH.Application.Interface.Customer;
using ORH.Application.UseCase.Customer.Commands;
using ORH.Domain.Repositories;

namespace ORH.Infrastructure.Implementation.Customer
{
    [Service(ServiceLifetime.Scoped)]
    public class PurchaseProductService(IUnitOfWork context) : IPurchaseProduct
    {
        private readonly IUnitOfWork _context = context;

        public async Task<bool> IsProductInStockAsync(PurchaseProductRequest request)
        {
            return await _context.ProductRepository.IsProductInStockAsync(request.ProductId, request.Quantity);
        }

        public async Task<PurchaseProductResponse> ExecutePurchaseProductAsync(PurchaseProductRequest request)
        {
            var result = new PurchaseProductResponse
            {
                PurchasedDate = DateTime.UtcNow
            };

            return result;
        }
    }
}
