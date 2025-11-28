using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORH.Application.UseCase.Customer.Queries;
using ORH.Application.Interface.Customer;
using ORH.Domain.Repositories;

namespace ORH.Infrastructure.Implementation.Customer
{
    [Service(ServiceLifetime.Scoped)]
    public class SearchCustomersService(IUnitOfWork context) : ISearchCustomers
    {
        private readonly IUnitOfWork _context = context;

        public async Task<List<SearchCustomersResponse>> ExecuteSearchCustomersAsync(SearchCustomersRequest request)
        {
            var query = _context.CustomerRepository.GetCustomersQueryable();

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(c => string.Concat(c.FirstName, c.LastName).ToLower().Contains(request.Name.ToLower()));
                }

                if (!string.IsNullOrEmpty(request.Email))
                {
                    query = query.Where(c => c.Email.Contains(request.Email));
                }

                if (!string.IsNullOrEmpty(request.Phone))
                {
                    query = query.Where(c => c.Phone != null && c.Phone.Contains(request.Phone));
                }

                if (!string.IsNullOrEmpty(request.Address))
                {
                    query = query.Where(c => c.Address != null && c.Address.Contains(request.Address));
                }
            }

            var customers = await query.ToListAsync();
            var result = customers
                .Select(c => new SearchCustomersResponse
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Gender = c.Gender,
                    Phone = c.Phone,
                    Address = c.Address
                })
                .ToList();

            return result;
        }
    }
}
