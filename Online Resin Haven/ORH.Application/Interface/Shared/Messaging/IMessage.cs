using MassTransit;
using MediatR;

namespace ORH.Application.Interface.Shared.Messaging
{
    [ExcludeFromTopology]
    public interface IMessage : IRequest
    {
        Guid Id { get; init; }
        DateTimeOffset TimeStamp { get; init; }
    }
}
