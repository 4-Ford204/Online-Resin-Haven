using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORH.Application.Interface.Customer;
using ORH.Application.UseCase.Customer.Queries;
using ORH.Domain.Repositories;

namespace ORH.Infrastructure.Implementation.Customer
{
    [Service(ServiceLifetime.Scoped)]
    public class LoginService(IUnitOfWork context) : ILogin
    {
        private readonly IUnitOfWork _context = context;

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _context.CustomerRepository.IsEmailExistAsync(email);
        }

        public async Task<LoginResponse> ExecuteLoginAsync(LoginRequest request)
        {
            var customer = await _context.CustomerRepository
                .GetCustomersQueryable()
                .FirstOrDefaultAsync(c => c.Email == request.Email && c.Password == request.Password) ??
                throw new UnauthorizedAccessException("Invalid email or password.");

            var accessToken = "generated_access_token";
            var refreshToken = "generated_refresh_token";

            return new LoginResponse
            {
                Name = $"{customer.FirstName} {customer.LastName}",
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
