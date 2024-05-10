using DomainEvent.Core;
using DomainEvent.Model;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace DomainEvent.Services;

public class PersonServices
{
    private readonly DomainEventContext _context;
    public PersonServices(DomainEventContext context)
    {
        _context = context;
    }
    
    public async Task CreatePerson(string name,string family)
    {
        var newPerson = new Person(name,family);
        await _context.AddAsync(newPerson);
        await _context.SaveChangesAsync();
    }
    
    public async Task ChangeName(string name,long id)
    {
        var person = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
        person.ChangeNameEntity(name);
        await _context.SaveChangesAsync();
    }
    public async Task ChangeFamily(string family,long id)
    {
        var person = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
        person.ChangeNameEntity(family);
        await _context.SaveChangesAsync();
    }
}