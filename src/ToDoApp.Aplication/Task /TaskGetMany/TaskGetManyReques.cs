using ToDoApp.Application.Core;
using ToDoApp.Domain;

namespace ToDoApp.Application.Task;

public class TaskGetManyRequest : PagingParams
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
}

