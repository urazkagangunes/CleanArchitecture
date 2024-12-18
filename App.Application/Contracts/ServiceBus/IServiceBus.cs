using App.Domain.Events;

namespace App.Application.Contracts.ServiceBus
{
    public interface IServiceBus
    {
        Task PublishAsync<T> (T @event, CancellationToken cancellationToken = default) where T : IEventOrMessage;
        Task SendAsync<T> (T Message, string queueName, CancellationToken cancellationToken = default) where T : IEventOrMessage;
    }
}