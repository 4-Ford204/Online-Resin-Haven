using ORH.Domain.Entities;

namespace ORH.Domain.Repositories
{
    public interface ICustomerRepository
    {
        public Task<bool> IsEmailExistAsync(string email);
        public IQueryable<Customer> GetCustomersQueryable();
        public Task CreateCustomerAsync(Customer customer);

    }
}
