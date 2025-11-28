using Ardalis.Result;
using Ardalis.SharedKernel;
using ORH.Application.Interface.Customer;
using ORH.Domain.Constant.Enums;

namespace ORH.Application.UseCase.Customer.Queries
{
    public record SearchCustomersQuery(SearchCustomersRequest Request) : IQuery<Result<List<SearchCustomersResponse>>>;

    public class SearchCustomersHandler(ISearchCustomers service) : IQueryHandler<SearchCustomersQuery, Result<List<SearchCustomersResponse>>>
    {
        public async Task<Result<List<SearchCustomersResponse>>> Handle(SearchCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customers = await service.ExecuteSearchCustomersAsync(request.Request);
                return Result.Success(customers);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }

    public class SearchCustomersRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }

    public class SearchCustomersResponse
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required Gender Gender { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
