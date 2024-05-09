using System.ComponentModel;
using Framework.Domain;

namespace DomainEvent.Core.Event.Event;

public class ChangeName : IDomainEvent
{
    public long Id { get; set; }
    public string Name { get; set; }

    public ChangeName(long id, string name)
    {
        Id = id;
        Name = name;
    }
}