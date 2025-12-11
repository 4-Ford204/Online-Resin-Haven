using Ardalis.Result;
using Ardalis.SharedKernel;
using ORH.Application.Interface.Customer;

namespace ORH.Application.UseCase.Customer.Queries
{
    public record LoginQuery(LoginRequest Request) : IQuery<Result<LoginResponse>>;

    public class LoginHandler(ILogin service) : IQueryHandler<LoginQuery, Result<LoginResponse>>
    {
        public async Task<Result<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var isEmailExist = await service.IsEmailExistAsync(request.Request.Email);

                if (!isEmailExist)
                {
                    return Result.Error("Email does not exist.");
                }

                var customer = await service.ExecuteLoginAsync(request.Request);

                return Result.Success(customer);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }

    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class LoginResponse
    {
        public required string Name { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
