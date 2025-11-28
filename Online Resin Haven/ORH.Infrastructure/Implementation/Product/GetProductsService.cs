using Microsoft.Extensions.DependencyInjection;
using ORH.Application.UseCase.Product.Queries;
using ORH.Application.Interface.Product;
using ORH.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ORH.Infrastructure.Implementation.Product
{
    [Service(ServiceLifetime.Scoped)]
    public class GetProductsService(IUnitOfWork context) : IGetProducts
    {
        private readonly IUnitOfWork _context = context;

        public async Task<List<GetProductsResponse>> ExecuteGetProductsAsync()
        {
            var result = await
                (
                    from product in _context.ProductRepository.GetProductsQueryable()
                    join character in _context.CharacterRepository.GetCharactersQueryable()
                        on product.CharacterId equals character.Id
                    join line in _context.LineRepository.GetLinesQueryable()
                        on character.LineId equals line.Id
                    join studio in _context.StudioRepository.GetStudiosQueryable()
                        on product.StudioId equals studio.Id
                    join productImage in _context.ProductImageRepository.GetProductImagesQueryable()
                        on product.Id equals productImage.ProductId into productImages
                    select new GetProductsResponse
                    {
                        Id = product.Id,
                        CharacterName = character.Name,
                        StudioName = studio.Name,
                        Name = product.Name,
                        Quantity = product.Quantity,
                        Price = product.Price,
                        Images = productImages.Select(pi => pi.URL).AsEnumerable()
                    }
                )
                .ToListAsync();

            return result;
        }
    }
}
