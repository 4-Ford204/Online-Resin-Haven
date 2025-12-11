using Customer.API.Abstraction.Endpoints;
using MediatR;
using ORH.Application.UseCase.Customer.Queries;

namespace Customer.API.Endpoints
{
    public class LoginEndpoint : BaseEndpoint<LoginRequest, LoginResponse>
    {
        public LoginEndpoint(IMediator mediator) : base(mediator) { }

        public override void Configure()
        {
            Post("/customer/login");
            AllowAnonymous();
            Description(b => b
                .WithTags("Customer Endpoints")
                .Produces<LoginResponse>(200)
                .Produces(400)
                .Produces(500)
            );
        }

        public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
        {
            var query = new LoginQuery(request);
            var result = await _mediator.Send(query, ct);

            await HandleResultAsync(result, ct);
        }
    }
}
