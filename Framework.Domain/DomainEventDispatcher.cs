using Microsoft.Extensions.DependencyInjection;

namespace Framework.Domain;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _service;

    public DomainEventDispatcher(IServiceProvider service)
    {
        _service = service;
    }

    public async Task Dispatch(IEnumerable<IDomainEvent> events)
    {
        foreach (dynamic @event in events)
        {
            DispatchEvent(@event);
        }
    }
    
    private async Task DispatchEvent<TDomainEvent>(TDomainEvent @event)
        where TDomainEvent: IDomainEvent
    {
        var type = typeof(TDomainEvent).GetType();
        var handlers= _service.GetServices<IDomainEventHandler<TDomainEvent>>();
        foreach (var handler in handlers)
        {
            await handler.Handle(@event);
        }
    }
}