using ORH.Application.UseCase.Customer.Commands;

namespace ORH.Application.Interface.Customer
{
    public interface ICreateCustomer
    {
        Task<bool> IsEmailExistAsync(string email);
        Task<CreateCustomerResponse> ExecuteCreateCustomerAsync(CreateCustomerRequest request);
    }
}
