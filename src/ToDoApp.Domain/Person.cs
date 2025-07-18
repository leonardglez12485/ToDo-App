namespace ToDoApp.Domain;

public class Person : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public ICollection<Taske> Taske { get; set; } = new List<Taske>();

    public void AddTask(Taske task)
    {
        Taske.Add(task);
    }
}