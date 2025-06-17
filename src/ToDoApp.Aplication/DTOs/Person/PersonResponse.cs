
using ToDoApp.Aplication.DTOs.Task;

namespace ToDoApp.Aplication.DTOs.Person;

public record Personresponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public ICollection<TaskResponse>? Tasks { get; set; }
}

