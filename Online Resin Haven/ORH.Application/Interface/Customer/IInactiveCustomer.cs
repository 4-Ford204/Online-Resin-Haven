using ORH.Application.UseCase.Customer.Commands;

namespace ORH.Application.Interface.Customer
{
    public interface IInactiveCustomer
    {
        Task<bool> IsActiveCustomerAsync(int id);
        Task<InactiveCustomerResponse> ExecuteInactiveCustomerAsync(InactiveCustomerRequest request);
    }
}
