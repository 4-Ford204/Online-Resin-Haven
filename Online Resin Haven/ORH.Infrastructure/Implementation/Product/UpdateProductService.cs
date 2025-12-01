using Microsoft.Extensions.DependencyInjection;
using ORH.Application.Interface.Product;
using ORH.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ORH.Infrastructure.Implementation.Product
{
    [Service(ServiceLifetime.Scoped)]
    public class UpdateProductService(IUnitOfWork context) : IUpdateProduct
    {
        private readonly IUnitOfWork _context = context;

        public async Task<bool> ExecuteUpdateProductQuantityAsync(int id, int quantity)
        {
            var product = await _context.ProductRepository.GetProductsQueryable(id: id).FirstOrDefaultAsync(p => p.Quantity >= quantity);

            if (product == null)
            {
                return false;
            }

            product.Quantity -= quantity;

            var result = await _context.SaveChangesAsync().ContinueWith(x => x.Result == 1);

            return result;
        }
    }
}
