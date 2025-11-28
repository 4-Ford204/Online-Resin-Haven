using MediatR;
using ORH.Application.UseCase.Product.IntegrationEvents;
using Product.API.Abstraction.Consumers;

namespace Product.API.Consumer.Product
{
    public class ProductPurchasedConsumer : BaseConsumer<ProductPurchasedIntegrationEvent>
    {
        public ProductPurchasedConsumer(ISender sender) : base(sender) { }
    }
}
