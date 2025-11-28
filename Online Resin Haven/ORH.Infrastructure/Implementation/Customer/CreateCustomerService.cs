using Microsoft.Extensions.DependencyInjection;
using ORH.Application.Interface.Customer;
using ORH.Application.UseCase.Customer.Commands;
using ORH.Infrastructure.Implementation.Repositories;

namespace ORH.Infrastructure.Implementation.Customer
{
    [Service(ServiceLifetime.Scoped)]
    public class CreateCustomerService(UnitOfWork context) : ICreateCustomer
    {
        private readonly UnitOfWork _context = context;

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _context.CustomerRepository.IsEmailExistAsync(email);
        }

        public async Task<CreateCustomerResponse> ExecuteCreateCustomerAsync(CreateCustomerRequest request)
        {
            var customer = new Domain.Entities.Customer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                Gender = request.Gender,
                Phone = request.Phone,
                Address = request.Address
            };

            await _context.CustomerRepository.CreateCustomerAsync(customer);
            await _context.SaveChangesAsync();

            var result = new CreateCustomerResponse
            {
                Name = $"{customer.FirstName} {customer.LastName}"
            };

            return result;
        }
    }
}
