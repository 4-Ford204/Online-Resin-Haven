using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORH.Application.Interface.Customer;
using ORH.Application.UseCase.Customer.Commands;
using ORH.Domain.Constant.Enums;
using ORH.Domain.Repositories;

namespace ORH.Infrastructure.Implementation.Customer
{
    [Service(ServiceLifetime.Scoped)]
    public class InactiveCustomerService(IUnitOfWork context) : IInactiveCustomer
    {
        private readonly IUnitOfWork _context = context;

        public async Task<bool> IsActiveCustomerAsync(int id)
        {
            return await _context.CustomerRepository.GetCustomersQueryable(id: id).AnyAsync();
        }

        public async Task<InactiveCustomerResponse> ExecuteInactiveCustomerAsync(InactiveCustomerRequest request)
        {
            var customer = await _context.CustomerRepository.GetCustomersQueryable().FirstAsync(c => c.Id == request.CustomerId);
            var result = new InactiveCustomerResponse()
            {
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                Email = customer.Email,
                Gender = customer.Gender,
                Phone = customer.Phone,
                Address = customer.Address,
                IsInactive = false
            };

            if (customer != null)
            {
                customer.Status = Status.Inactive;

                var isInactive = await _context.SaveChangesAsync().ContinueWith(x => x.Result == 1);

                if (isInactive)
                {
                    result.IsInactive = true;
                }
            }

            return result;
        }
    }
}
