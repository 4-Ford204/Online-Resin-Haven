using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORH.Application.UseCase.Studio.Queries;
using ORH.Application.Interface.Studio;
using ORH.Domain.Repositories;

namespace ORH.Infrastructure.Implementation.Studio
{
    [Service(ServiceLifetime.Scoped)]
    public class GetStudiosService(IUnitOfWork context) : IGetStudios
    {
        private readonly IUnitOfWork _context = context;

        public async Task<List<GetStudiosResponse>> ExecuteGetStudiosAsync()
        {
            var result = await _context.StudioRepository.GetStudiosQueryable()
                .GroupJoin(
                    _context.ProductRepository.GetProductsQueryable(),
                    studio => studio.Id,
                    product => product.StudioId,
                    (studio, products) => new GetStudiosResponse
                    {
                        Id = studio.Id,
                        Name = studio.Name,
                        ProductNumber = products.Count()
                    }
                )
                .ToListAsync();

            return result;
        }
    }
}
