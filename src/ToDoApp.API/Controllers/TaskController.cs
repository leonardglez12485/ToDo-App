using Microsoft.AspNetCore.Mvc;
using MediatR;


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
    public async Task<ActionResult<IEnumerable<Task>>> GetAllTask()
    {

    }

    [HttpPost]
    public async Task<ActionResult<Task>> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        
    }
}
   