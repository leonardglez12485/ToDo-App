using Microsoft.AspNetCore.Mvc;
using MediatR;
using ToDoApp.Aplication.Task.TaskCreate;
using ToDoApp.Aplication.DTOs.Task;
using ToDoApp.Application.Core;
using ToDoApp.Application.Task.TaskGestMany;
using ToDoApp.Application.Task;
using ToDoApp.Aplication.Task.TaskAsignPerson;


namespace ToDoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ISender _sender;

    public TaskController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<PageList<TaskResponse>>> GetAllTask(
        [FromQuery] TaskGetManyRequest? taskGetManyRequest,
        CancellationToken cancellationToken = default
    )
    {
        var quesry = new TaskGestManyQuery.TaskGetManyQueryRequest
        {
            taskGetManyRequest = taskGetManyRequest
        };
        var response = await _sender.Send(quesry, cancellationToken);
        if (response is null)
            return NotFound("No tasks found.");

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> CreateTask(
        [FromBody] TaskCreateRequest request,
        CancellationToken cancellationToken = default
        )
    {
        var handler = new TaskCreateCommandRequest(request);
        var response = await _sender.Send(handler, cancellationToken);
        if (response is null)
            return BadRequest("Failed to create task.");

        return Ok(response);
    }

    [HttpPost("assign-person")]
    public async Task<ActionResult<TaskResponse>> AssignPersonToTask(
        [FromBody] TaskAsignPersonRequest request,
        CancellationToken cancellationToken = default
    )
    {
        if (request is null)
            return BadRequest("Task assign person request cannot be null.");

        var command = new TaskAsignPersonCommandRequest(request);
        var response = await _sender.Send(command, cancellationToken);
        
        if (response is null)
            return NotFound($"Task with ID {request.Equals} does not exist or could not be assigned.");

        return Ok(response);
    }
}
   