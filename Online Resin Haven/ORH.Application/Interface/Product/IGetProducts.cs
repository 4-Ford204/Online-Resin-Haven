using ORH.Application.UseCase.Product.Queries;

namespace ORH.Application.Interface.Product
{
    public interface IGetProducts
    {
        Task<List<GetProductsResponse>> ExecuteGetProductsAsync();
    }
}
