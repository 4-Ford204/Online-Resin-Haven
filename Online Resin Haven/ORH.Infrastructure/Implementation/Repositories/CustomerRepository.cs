using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORH.Domain.Constant.Enums;
using ORH.Domain.Repositories;
using ORH.Infrastructure.DatabaseContext;

namespace ORH.Infrastructure.Implementation.Repositories
{
    [Service(ServiceLifetime.Scoped)]
    public class CustomerRepository(OnlineResinHaven dbContext) : ICustomerRepository
    {
        private readonly OnlineResinHaven _dbContext = dbContext;

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _dbContext.Customers.AnyAsync(c => c.Email.Equals(email));
        }

        public IQueryable<Domain.Entities.Customer> GetCustomersQueryable(int id = 0)
        {
            return _dbContext.Customers.Where(c => (id <= 0 || c.Id == id) && c.Status == Status.Active).AsQueryable();
        }

        public async Task CreateCustomerAsync(Domain.Entities.Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
        }
    }
}
