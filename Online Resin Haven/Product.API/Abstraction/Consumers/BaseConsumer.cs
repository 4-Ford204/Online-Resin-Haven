using MassTransit;
using MediatR;
using ORH.Application.Interface.Shared.Messaging;

namespace Product.API.Abstraction.Consumers
{
    public abstract class BaseConsumer<TMessage> : IConsumer<TMessage> where TMessage : IntegrationEvent
    {
        protected ISender _sender;

        protected BaseConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<TMessage> context)
        {
            await _sender.Send(context.Message, context.CancellationToken);
        }
    }
}
