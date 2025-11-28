using ORH.Domain.Entities;

namespace ORH.Domain.Repositories
{
    public interface ICharacterRepository
    {
        IQueryable<Character> GetCharactersQueryable();
    }
}
