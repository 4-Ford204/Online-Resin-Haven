using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORH.Application.Interface.Character;
using ORH.Application.UseCase.Character.Queries;
using ORH.Domain.Repositories;

namespace ORH.Infrastructure.Implementation.Character
{
    [Service(ServiceLifetime.Scoped)]
    public class GetCharactersService(IUnitOfWork context) : IGetCharacters
    {
        private readonly IUnitOfWork _context = context;

        public async Task<List<GetCharactersResponse>> ExecuteGetCharactersAsync()
        {
            var result = await _context.CharacterRepository.GetCharactersQueryable()
                .GroupJoin(
                    _context.ProductRepository.GetProductsQueryable(),
                    character => character.Id,
                    product => product.CharacterId,
                    (character, products) => new GetCharactersResponse
                    {
                        Id = character.Id,
                        Name = character.Name,
                        ProductNumber = products.Count()
                    }
                )
                .ToListAsync();

            return result;
        }
    }
}
