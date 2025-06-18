using AutoMapper;
using MediatR;
using ToDoApp.Aplication.DTOs.Task;
using ToDoApp.Domain;
using ToDoApp.Infrastructure.Presistence;

namespace ToDoApp.Aplication.Task.TaskCreate;

public record TaskCreateCommandRequest(TaskCreateRequest taskCreateRequest) 
    : IRequest<TaskResponse>;

internal class TaskCreateCommandHandler
    : IRequestHandler<TaskCreateCommandRequest, TaskResponse>
{
    private readonly ToDoDbContext _context;
    private readonly IMapper _mapper;

    public TaskCreateCommandHandler(ToDoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaskResponse> Handle(TaskCreateCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.taskCreateRequest is null)
        {
            throw new ArgumentNullException(nameof(request.taskCreateRequest), "Task create request cannot be null");
        }
        var task = _mapper.Map<Taske>(request.taskCreateRequest);
        task.Id = Guid.NewGuid();
        task.CreatedAt = DateTime.UtcNow;
        task.Status = CustomStatus.Open;
        _context.Taske!.Add(task);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result <= 0)
            throw new InvalidOperationException("Failed to create task.");      

        return _mapper.Map<TaskResponse>(task);
    }
}