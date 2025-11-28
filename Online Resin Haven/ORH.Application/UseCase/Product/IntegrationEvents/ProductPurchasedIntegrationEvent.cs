using MediatR;
using ORH.Application.Interface.Product;
using ORH.Application.Interface.Shared.Messaging;

namespace ORH.Application.UseCase.Product.IntegrationEvents
{
    public record ProductPurchasedIntegrationEvent : IntegrationEvent
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductPurchasedIntegrationEventHandler(IUpdateProduct service) : IRequestHandler<ProductPurchasedIntegrationEvent>
    {
        public async Task Handle(ProductPurchasedIntegrationEvent request, CancellationToken cancellationToken)
        {
            await service.ExecuteUpdateProductQuantityAsync(request.ProductId, request.Quantity);
        }
    }
}
