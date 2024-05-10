using DomainEvent.Core.Event.Event;
using Framework.Domain;
using Newtonsoft.Json;

namespace DomainEvent.Services.EventHandler;

public class WriteCreatePersonToConsole : IDomainEventHandler<Createdperson>
{
    public Task Handle(Createdperson domainEvent)
    {
        var str = JsonConvert.SerializeObject(domainEvent);
        Console.WriteLine(str);
        return Task.CompletedTask;
    }
}