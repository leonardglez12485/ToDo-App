namespace ToDoApp.Aplication.Task.TaskAsignPerson;

public class TaskAsignPersonRequest
{
    public Guid TaskId { get; init; }
    public Guid PersonId { get; init; }
}