using MassTransit;

namespace ORH.Application.Interface.Shared.Messaging
{
    [ExcludeFromTopology]
    public abstract record IntegrationEvent : IMessage
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTimeOffset TimeStamp { get; init; } = DateTimeOffset.UtcNow;
    }
}
