using Ardalis.Result;
using Ardalis.SharedKernel;
using ORH.Application.Interface.Product;

namespace ORH.Application.UseCase.Product.Queries
{
    public record GetProductsQuery : IQuery<Result<List<GetProductsResponse>>>;

    public class GetProductsHandler(IGetProducts service) : IQueryHandler<GetProductsQuery, Result<List<GetProductsResponse>>>
    {
        public async Task<Result<List<GetProductsResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var pets = await service.ExecuteGetProductsAsync();
                return Result.Success(pets);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }


    public class GetProductsResponse
    {
        public required int Id { get; set; }
        public required string CharacterName { get; set; }
        public required string StudioName { get; set; }
        public required string Name { get; set; }
        public required int Quantity { get; set; }
        public required float Price { get; set; }
        public IEnumerable<string>? Images { get; set; }
    }
}
