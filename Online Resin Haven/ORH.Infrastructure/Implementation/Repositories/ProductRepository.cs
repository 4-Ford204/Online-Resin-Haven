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

        public Task<bool> IsProductInStockAsync(int id, int quantity)
        {
            return _dbContext.Products.AnyAsync(p => p.Id == id && p.Quantity >= quantity);
        }

        public IQueryable<Domain.Entities.Product> GetProductsQueryable()
        {
            return _dbContext.Products.Where(p => p.Status == Status.Active).AsQueryable();
        }
    }
}
