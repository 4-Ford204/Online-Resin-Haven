using ORH.Domain.Entities;

namespace ORH.Domain.Repositories
{
    public interface IStudioRepository
    {
        IQueryable<Studio> GetStudiosQueryable();
    }
}
