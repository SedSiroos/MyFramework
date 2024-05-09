namespace Framework.Domain;

public abstract class Entity<T>
{
    public T Id { get; set; }
    
    private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
    protected void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }
    
    public IReadOnlyList<IDomainEvent> Events => _events;
    
}