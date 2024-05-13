using DomainEvent.Model;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;

namespace DomainEvent.Core;

public class DomainEventContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<OutBoxEventItem> OutBoxEvent { get; set; }
    
    public DomainEventContext(DbContextOptions<DomainEventContext> options):base(options){}
    
    public override int SaveChanges()
    {
        HandleBeforeSaveChanges();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        HandleBeforeSaveChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        HandleBeforeSaveChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        HandleBeforeSaveChanges();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void HandleBeforeSaveChanges()
    {
        AddToOutBox();
        DispatchEvents();
    }


    private void AddToOutBox()
    {
        var entities = ChangeTracker.Entries<Entity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified)
            .Select(x => x.Entity).ToList();

        var dateTime = DateTime.Now;
        foreach (var entity in entities)
        {
            foreach (var @event in entity.Events)
            {
                OutBoxEvent.Add(new OutBoxEventItem
                {
                    EventId = Guid.NewGuid(),
                    AccuredUserId = Guid.NewGuid().ToString(),
                    AccuredOn = dateTime,
                    AggragateId = Guid.NewGuid().ToString(),
                    AggragateName = entity.GetType().Name,
                    AggragateTypeName = entity.GetType().FullName,
                    EventName = @event.GetType().Name,
                    EventTypeName = @event.GetType().FullName,
                    EventPayload = JsonConvert.SerializeObject(@event),
                    IsProcessed = false
                });
            }
        }
    }
    
    private void DispatchEvents()
    {
        var dispatch = this.GetService<IDomainEventDispatcher>();
        var domainEvent = ChangeTracker
            .Entries<Entity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified)
            .Select(x=>x.Entity)
            .ToList();

        foreach (var @event in domainEvent)
        {
            dispatch.Dispatch(@event.Events);
            @event.ClearEvent();
        }
    }
}