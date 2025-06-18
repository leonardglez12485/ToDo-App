namespace ToDoApp.Domain;

public class Taske : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = CustomStatus.Open;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }

    public void AssignPerson(Guid personId, Person person)
    {
        if (Status != CustomStatus.Open)
            throw new InvalidOperationException("Only open tasks can be assigned.");
        if (PersonId != null)
            throw new InvalidOperationException("Task is already assigned to a person.");

        PersonId = personId;
        Person= person;
        Status = CustomStatus.InProgress;
    }
}