using ToDoApp.Domain;

namespace ToDoApp.Aplication.DTOs.Task;

public class TaskResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = CustomStatus.Open;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public string PersonId { get; set; }


    public TaskResponse()
    {
        Title = string.Empty;
        Description = string.Empty;
        Status = CustomStatus.Open;
        CreatedAt = DateTime.UtcNow;
        PersonId = string.Empty;
    }
}

