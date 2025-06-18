using AutoMapper;
using MediatR;
using ToDoApp.Aplication.DTOs.Task;
using ToDoApp.Domain;
using ToDoApp.Infrastructure.Presistence;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ToDoApp.Aplication.Task.TaskAsignPerson;

public record TaskAsignPersonCommandRequest(TaskAsignPersonRequest taskAsignPersonRequest) 
    : IRequest<TaskResponse>;

internal class TaskAsignPersonCommandHandler
    : IRequestHandler<TaskAsignPersonCommandRequest, TaskResponse>
{
    private readonly ToDoDbContext _context;
    private readonly IMapper _mapper;

    public TaskAsignPersonCommandHandler(ToDoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaskResponse> Handle(TaskAsignPersonCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.taskAsignPersonRequest is null)
            throw new ArgumentNullException(nameof(request.taskAsignPersonRequest), "Task assign person request cannot be null");
        var task = await _context.Taske!.FirstOrDefaultAsync(t => t.Id == request.taskAsignPersonRequest.TaskId, cancellationToken);
       // var task = await _context.Taske!.FirstOrDefault(t => t.Id == request.taskAsignPersonRequest.TaskId);
        if (task is null)
            throw new InvalidOperationException($"Task with ID {request.taskAsignPersonRequest.TaskId} does not exist.");

        var person = await _context.Person!.FirstOrDefaultAsync(p => p.Id == request.taskAsignPersonRequest.PersonId, cancellationToken);
        if (person is null)
            throw new InvalidOperationException($"Person with ID {request.taskAsignPersonRequest.PersonId} does not exist.");
        
        try
        {
            task.AssignPerson(request.taskAsignPersonRequest.PersonId, person);
             _context.Taske!.Update(task);
            var result = await _context.SaveChangesAsync();
            if (result <= 0)
                throw new InvalidOperationException("Failed to assign person to task.");
            
            return _mapper.Map<TaskResponse>(task);
        }
        catch (System.Exception)
        {
            throw new InvalidOperationException("Failed to assign person to task. Ensure the task is open and not already assigned.");
        }
    }
}