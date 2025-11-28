using ORH.Domain.Entities;

namespace ORH.Domain.Repositories
{
    public interface IProductImageRepository
    {
        IQueryable<ProductImage> GetProductImagesQueryable();
    }
}
