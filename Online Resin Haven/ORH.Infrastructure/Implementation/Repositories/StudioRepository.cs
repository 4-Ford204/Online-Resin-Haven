using Microsoft.Extensions.DependencyInjection;
using ORH.Domain.Constant.Enums;
using ORH.Domain.Repositories;
using ORH.Infrastructure.DatabaseContext;

namespace ORH.Infrastructure.Implementation.Repositories
{
    [Service(ServiceLifetime.Scoped)]
    public class StudioRepository(OnlineResinHaven dbContext) : IStudioRepository
    {
        private readonly OnlineResinHaven _dbContext = dbContext;

        public IQueryable<Domain.Entities.Studio> GetStudiosQueryable()
        {
            return _dbContext.Studios.Where(s => s.Status == Status.Active).AsQueryable();
        }
    }
}
