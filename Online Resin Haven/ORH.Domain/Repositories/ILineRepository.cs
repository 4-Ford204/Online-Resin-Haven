using ORH.Domain.Entities;

namespace ORH.Domain.Repositories
{
    public interface ILineRepository
    {
        IQueryable<Line> GetLinesQueryable();
    }
}
