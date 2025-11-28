using Microsoft.Extensions.DependencyInjection;
using ORH.Domain.Constant.Enums;
using ORH.Domain.Entities;
using ORH.Domain.Repositories;
using ORH.Infrastructure.DatabaseContext;

namespace ORH.Infrastructure.Implementation.Repositories
{
    [Service(ServiceLifetime.Scoped)]
    public class ProductImageRepository(OnlineResinHaven dbContext) : IProductImageRepository
    {
        private readonly OnlineResinHaven _dbContext = dbContext;

        public IQueryable<ProductImage> GetProductImagesQueryable()
        {
            return _dbContext.ProductImages.Where(pi => pi.Status == Status.Active).AsQueryable();
        }
    }
}
