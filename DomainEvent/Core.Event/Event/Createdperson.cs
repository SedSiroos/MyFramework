using Framework.Domain;

namespace DomainEvent.Core.Event.Event;

public class Createdperson : IDomainEvent
{
    public string Name { get; set; }
    public string Family { get; set; }

    public Createdperson(string name, string family)
    {
        Name = name;
        Family = family;
    }
}