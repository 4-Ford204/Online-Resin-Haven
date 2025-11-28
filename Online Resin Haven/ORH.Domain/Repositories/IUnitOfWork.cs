namespace ORH.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public ICharacterRepository CharacterRepository { get; }
        public ILineRepository LineRepository { get; }
        public IProductImageRepository ProductImageRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IStudioRepository StudioRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
