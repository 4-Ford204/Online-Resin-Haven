using Ardalis.Result;
using Ardalis.SharedKernel;
using ORH.Application.Interface.Customer;
using ORH.Domain.Constant.Enums;

namespace ORH.Application.UseCase.Customer.Commands
{
    public record CreateCustomerCommand(CreateCustomerRequest Request) : ICommand<Result<CreateCustomerResponse>>;

    public class CreateCustomerHandler(ICreateCustomer service) : ICommandHandler<CreateCustomerCommand, Result<CreateCustomerResponse>>
    {
        public async Task<Result<CreateCustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var isEmailExist = await service.IsEmailExistAsync(request.Request.Email);

                if (isEmailExist)
                {
                    return Result.Invalid(new ValidationError()
                    {
                        Identifier = nameof(request.Request.Email),
                        ErrorMessage = "Email already exists."
                    });
                }

                var customer = await service.ExecuteCreateCustomerAsync(request.Request);
                
                return Result.Success(customer);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }

    public class CreateCustomerRequest()
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Gender Gender { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }

    public class CreateCustomerResponse
    {
        public required string Name { get; set; }
    }
}
