using Framework.Domain;

namespace DomainEvent.Core.Event.Event;

public class ChangeFamily : IDomainEvent
{
    public long Id { get; set; }
    public string Family { get; set; }

    public ChangeFamily(long id, string family)
    {
        Id = id;
        Family = family;
    }
}