using FastEndpoints;
using FluentValidation;
using ORH.Application.UseCase.Customer.Queries;

namespace Customer.API.Endpoints
{
    public class LoginEndpointRequestValidator : Validator<LoginRequest>
    {
        public LoginEndpointRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
