using ORH.Domain.Entities;

namespace ORH.Domain.Repositories
{
    public interface ICustomerRepository
    {
        public Task<bool> IsEmailExistAsync(string email);
        public IQueryable<Customer> GetCustomersQueryable(int id = 0);
        public Task CreateCustomerAsync(Customer customer);
    }
}
