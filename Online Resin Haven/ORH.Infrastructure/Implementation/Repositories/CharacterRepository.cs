using Microsoft.Extensions.DependencyInjection;
using ORH.Domain.Constant.Enums;
using ORH.Domain.Repositories;
using ORH.Infrastructure.DatabaseContext;

namespace ORH.Infrastructure.Implementation.Repositories
{
    [Service(ServiceLifetime.Scoped)]
    public class CharacterRepository(OnlineResinHaven dbContext) : ICharacterRepository
    {
        private readonly OnlineResinHaven _dbContext = dbContext;

        public IQueryable<Domain.Entities.Character> GetCharactersQueryable()
        {
            return _dbContext.Characters.Where(c => c.Status == Status.Active).AsQueryable();
        }
    }
}
