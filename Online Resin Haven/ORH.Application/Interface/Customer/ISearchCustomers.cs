using ORH.Application.UseCase.Customer.Queries;

namespace ORH.Application.Interface.Customer
{
    public interface ISearchCustomers
    {
        Task<List<SearchCustomersResponse>> ExecuteSearchCustomersAsync(SearchCustomersRequest request);
    }
}
