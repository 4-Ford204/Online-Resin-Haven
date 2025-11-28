using Microsoft.Extensions.DependencyInjection;
using ORH.Domain.Repositories;
using ORH.Infrastructure.DatabaseContext;

namespace ORH.Infrastructure.Implementation.Repositories
{
    [Service(ServiceLifetime.Scoped)]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineResinHaven _dbContext;
        private readonly Lazy<ICustomerRepository> _CustomerRepository;
        private readonly Lazy<ICharacterRepository> _CharacterRepository;
        private readonly Lazy<ILineRepository> _LineRepository;
        private readonly Lazy<IProductImageRepository> _ProductImageRepository;
        private readonly Lazy<IProductRepository> _ProductRepository;
        private readonly Lazy<IStudioRepository> _StudioRepository;

        public ICustomerRepository CustomerRepository => _CustomerRepository.Value;
        public ICharacterRepository CharacterRepository => _CharacterRepository.Value;
        public ILineRepository LineRepository => _LineRepository.Value;
        public IProductImageRepository ProductImageRepository => _ProductImageRepository.Value;
        public IProductRepository ProductRepository => _ProductRepository.Value;
        public IStudioRepository StudioRepository => _StudioRepository.Value;

        public UnitOfWork(OnlineResinHaven dbContext)
        {
            _dbContext = dbContext;
            _CustomerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(dbContext));
            _CharacterRepository = new Lazy<ICharacterRepository>(() => new CharacterRepository(dbContext));
            _LineRepository = new Lazy<ILineRepository>(() => new LineRepository(dbContext));
            _ProductImageRepository = new Lazy<IProductImageRepository>(() => new ProductImageRepository(dbContext));
            _ProductRepository = new Lazy<IProductRepository>(() => new ProductRepository(dbContext));
            _StudioRepository = new Lazy<IStudioRepository>(() => new StudioRepository(dbContext));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
