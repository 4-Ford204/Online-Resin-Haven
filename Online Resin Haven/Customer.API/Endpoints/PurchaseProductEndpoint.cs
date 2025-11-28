using Customer.API.Abstraction.Endpoints;
using MassTransit;
using MediatR;
using ORH.Application.UseCase.Customer.Commands;
using ORH.Application.UseCase.Product.IntegrationEvents;

namespace Customer.API.Endpoints
{
    public class PurchaseProductEndpoint : BaseEndpoint<PurchaseProductRequest, PurchaseProductResponse>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PurchaseProductEndpoint(IMediator mediator, IPublishEndpoint publishEndpoint) : base(mediator)
        {
            _publishEndpoint = publishEndpoint;
        }

        public override void Configure()
        {
            Post("/customer/purchase-pet");
            AllowAnonymous();
        }

        public override async Task HandleAsync(PurchaseProductRequest request, CancellationToken ct)
        {
            var command = new PurchaseProductCommand(request);
            var result = await _mediator.Send(command, ct);

            if (result.IsSuccess)
            {
                var petPurchasedIntegrationEvent = new ProductPurchasedIntegrationEvent
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };

                await _publishEndpoint.Publish(petPurchasedIntegrationEvent, ct);
            }

            await HandleResultAsync(result, ct);
        }
    }
}
