using ORH.Application.UseCase.Customer.Queries;

namespace ORH.Application.Interface.Customer
{
    public interface ILogin
    {
        Task<bool> IsEmailExistAsync(string email);
        Task<LoginResponse> ExecuteLoginAsync(LoginRequest request);
    }
}
