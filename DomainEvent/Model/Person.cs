using DomainEvent.Core.Event.Event;
using Framework.Domain;

namespace DomainEvent.Model;

public class Person : Entity
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }

    public Person( string name, string family)
    {
        Name = name;
        Family = family;
        AddEvent(new Createdperson(Name,Family));
    }

    public void ChangeNameEntity(string name)
    {
        Name = name;
        AddEvent(new ChangeName(Id,Name));
    }
    public void ChangeFamilyEntity( string family)
    {
        Family=family;
        AddEvent(new ChangeFamily(Id,Family));
    }
}