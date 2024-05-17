namespace Framework.Domain;

public class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
    public IReadOnlyList<IDomainEvent> Events => _events;
    public void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }
    public void ClearEvent()
    {
        _events.Clear();
    }
}