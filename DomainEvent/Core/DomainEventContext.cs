using DomainEvent.Model;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
        DispatchEvents();
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