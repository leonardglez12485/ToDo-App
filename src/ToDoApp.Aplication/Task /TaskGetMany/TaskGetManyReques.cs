using ToDoApp.Application.Core;
using ToDoApp.Domain;

namespace ToDoApp.Application.Task;

public class TaskGetManyRequest : PagingParams
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = CustomStatus.Open;  
}

