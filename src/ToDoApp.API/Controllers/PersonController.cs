using System.Drawing;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Aplication.DTOs.Person;
using ToDoApp.Aplication.Persons.PersonCreate;
using ToDoApp.Aplication.Persons.PersonDelete;
using ToDoApp.Aplication.Persons.PersonGetMany;
using ToDoApp.Aplication.Persons.PersonGetOne;
using ToDoApp.Aplication.Persons.PersonUpdate;
using ToDoApp.Application.Core;
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
    [HttpGet]
    public async Task<ActionResult<PageList<PersonResponse>>> GetMany(
        [FromQuery] PersonGetManyRequest? personGetManyRequest,
        CancellationToken cancellationToken = default
        )
    {
        var query = new PersonGetManyQuery.PersonGetMenyQueryRequest
        {
            personGetManyRequest = personGetManyRequest
        };
        var response = await _sender.Send(query, cancellationToken);

        if (response is null)
        {
            return NotFound("No persons found.");
        }

        return Ok(response);
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

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PersonResponse>> UpdatePerson(
        [FromBody] PersonUpdateRequest personUpdateRequest,
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var command = new PersonUpdateCommandRequest(personUpdateRequest, id);
        var updatet = await _sender.Send(command, cancellationToken);
        if (updatet is null)
        {
            return NotFound($"Person with ID {id} not found.");
        }
            return Ok(updatet);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<PersonResponse>> DeletePerson(
        Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var command = new PersonDeleteCommandRequest(id);
        var response = await _sender.Send(command, cancellationToken);
        if (response is null)
        {
            return NotFound($"Person with ID {id} not found.");
        }
        return Ok(response);   
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PersonResponse>> GetPersonById(
         Guid id,
        CancellationToken cancellationToken = default
        )
    {
        var personGetOneRequest = new PersonGetOneRequest
        {
            Id = id
        };
        var query = new PersonGetOneQueryRequest(personGetOneRequest);
        var response = await _sender.Send(query, cancellationToken);
        if (response is null)
        {
            return NotFound($"Person with ID {id} not found.");
        }
        return Ok(response);
    }

        
}