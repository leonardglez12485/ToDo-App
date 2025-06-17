using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Aplication.DTOs.Person;
using ToDoApp.Aplication.Persons.PersonCreate;
using static ToDoApp.Aplication.Persons.PersonCreate.PersonCreateCommand;

namespace ToDoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly ISender _sender;

    public PersonController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<PersonResponse>> CreatePerson(
        [FromBody] PersonCreateRequest personCreateRequest,
        CancellationToken cancellationToken = default
        )
    {
        var command = new PersonCreateCommandRequest(personCreateRequest);
        return await _sender.Send(command, cancellationToken);
    //     if (response is null)
    //     {
    //         return BadRequest("Failed to create person.");
    //     }
    //     // return response;
    //     return CreatedAtAction(nameof(CreatePerson), new { id = response.Id }, response);
    }
        
}