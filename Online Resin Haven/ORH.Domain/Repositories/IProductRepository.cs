using ORH.Domain.Entities;

namespace ORH.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<bool> IsProductInStockAsync(int id, int quantity);
        IQueryable<Product> GetProductsQueryable();
    }
}
