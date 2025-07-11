using ToDoApp.Application.Core;

namespace ToDoApp.Aplication.Persons.PersonGetMany;

public class PersonGetManyRequest : PagingParams
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

}

