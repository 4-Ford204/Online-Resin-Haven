using Ardalis.Result;
using Ardalis.SharedKernel;
using ORH.Application.Interface.Customer;
using ORH.Domain.Constant.Enums;

namespace ORH.Application.UseCase.Customer.Commands
{
    public record InactiveCustomerCommand(InactiveCustomerRequest Request) : ICommand<Result<InactiveCustomerResponse>>;

    public class InactiveCustomerHandler(IInactiveCustomer service) : ICommandHandler<InactiveCustomerCommand, Result<InactiveCustomerResponse>>
    {
        public async Task<Result<InactiveCustomerResponse>> Handle(InactiveCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var isActiveCustomer = await service.IsActiveCustomerAsync(request.Request.CustomerId);

                if (!isActiveCustomer)
                {
                    return Result.NotFound($"Customer with ID {request.Request.CustomerId} is already inactive or does not exist.");
                }

                var customer = await service.ExecuteInactiveCustomerAsync(request.Request);
                
                return Result.Success(customer);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }

    public class InactiveCustomerRequest
    {
        public required int CustomerId { get; set; }
    }

    public class InactiveCustomerResponse
    {
        public required string CustomerName { get; set; }
        public required string Email { get; set; }
        public required Gender Gender { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool IsInactive { get; set; }
    }
}
