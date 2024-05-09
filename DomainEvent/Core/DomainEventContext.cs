using DomainEvent.Model;
using Microsoft.EntityFrameworkCore;

namespace DomainEvent.Core;

public class DomainEventContext : DbContext
{
    public DbSet<Person> People { get; set; }
    
    public DomainEventContext(DbContextOptions<DomainEventContext> options):base(options){}
}