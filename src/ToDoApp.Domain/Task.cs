
using System.Reflection.Metadata;

namespace ToDoApp.Domain;

public class Task : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }
}