using Customer.API.Abstraction.Endpoints;
using MediatR;
using ORH.Application.UseCase.Customer.Commands;

namespace Customer.API.Endpoints
{
    public class InactiveCustomerEndpoint : BaseEndpoint<InactiveCustomerRequest, InactiveCustomerResponse>
    {
        public InactiveCustomerEndpoint(IMediator mediator) : base(mediator) { }

        public override void Configure()
        {
            Post("/customer/inactive");
            AllowAnonymous();
        }

        public override async Task HandleAsync(InactiveCustomerRequest request, CancellationToken ct)
        {
            var command = new InactiveCustomerCommand(request);
            var result = await _mediator.Send(command, ct);

            await HandleResultAsync(result, ct);
        }
    }
}
