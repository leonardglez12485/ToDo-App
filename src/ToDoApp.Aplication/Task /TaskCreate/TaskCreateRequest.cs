using ToDoApp.Domain;

namespace ToDoApp.Aplication.Task.TaskCreate;

public record TaskCreateRequest
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

