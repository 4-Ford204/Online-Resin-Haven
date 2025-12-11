using FastEndpoints;
using FluentValidation;
using ORH.Application.UseCase.Customer.Commands;

namespace Customer.API.Endpoints
{
    public class CreateCustomerRequestValidator : Validator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Gender).NotEmpty();
        }
    }
}
