using Microsoft.Extensions.DependencyInjection;
using ORH.Domain.Constant.Enums;
using ORH.Domain.Repositories;
using ORH.Infrastructure.DatabaseContext;

namespace ORH.Infrastructure.Implementation.Repositories
{
    [Service(ServiceLifetime.Scoped)]
    public class LineRepository(OnlineResinHaven dbContext) : ILineRepository
    {
        private readonly OnlineResinHaven _dbContext = dbContext;

        public IQueryable<Domain.Entities.Line> GetLinesQueryable()
        {
            return _dbContext.Lines.Where(l => l.Status == Status.Active).AsQueryable();
        }
    }
}
