namespace Framework.Domain;

public interface IDomainEventDispatcher
{
    Task Dispatch(IEnumerable<IDomainEvent> events);
}