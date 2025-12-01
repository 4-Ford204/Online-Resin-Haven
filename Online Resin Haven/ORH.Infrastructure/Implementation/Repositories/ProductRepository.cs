using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORH.Domain.Constant.Enums;
using ORH.Domain.Repositories;
using ORH.Infrastructure.DatabaseContext;

namespace ORH.Infrastructure.Implementation.Repositories
{
    [Service(ServiceLifetime.Scoped)]
    public class ProductRepository(OnlineResinHaven dbContext) : IProductRepository
    {
        private readonly OnlineResinHaven _dbContext = dbContext;

        public async Task<bool> IsProductInStockAsync(int id, int quantity)
        {
            return await _dbContext.Products.AnyAsync(p => p.Id == id && p.Quantity >= quantity);
        }

        public IQueryable<Domain.Entities.Product> GetProductsQueryable(int id = 0)
        {
            return _dbContext.Products.Where(p => (id <= 0 || p.Id == id) && p.Status == Status.Active).AsQueryable();
        }
    }
}
