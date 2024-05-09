using DomainEvent.Services;
using Microsoft.AspNetCore.Mvc;

namespace DomainEvent.Controllers;

[Route("[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly PersonServices _personServices;

    public PersonController(PersonServices personServices)
    {
        _personServices = personServices;
    }

    [HttpPost("Insert")]
    public async Task<IActionResult> CreatePerson(string name, string family)
    {
        await _personServices.CreatePerson(name, family);
        return Ok();
    }
    
    [HttpPost("ChangeName")]
    public async Task<IActionResult> ChangeName(string name,long id)
    {
        await _personServices.ChangeName(name, id);
        return Ok();
    }
    [HttpPost("ChangeFamily")]
    public async Task<IActionResult> ChangeFamily(long id, string family)
    {
        await _personServices.ChangeFamily(family,id);
        return Ok();
    }
}