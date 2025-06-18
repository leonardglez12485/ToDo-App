using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Aplication.DTOs.Person;
using ToDoApp.Domain;
using ToDoApp.Infrastructure.Presistence;

namespace ToDoApp.Aplication.Persons.PersonUpdate;

public record PersonUpdateCommandRequest(
    PersonUpdateRequest personUpdateRequest,
    Guid personId
    )
    : IRequest<PersonResponse>;

internal class PersonUpdateCommandHandler
    : IRequestHandler<PersonUpdateCommandRequest, PersonResponse>
{
    private readonly IMapper _mapper;
    private readonly ToDoDbContext _context;

    public PersonUpdateCommandHandler(IMapper mapper, ToDoDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PersonResponse> Handle(PersonUpdateCommandRequest request, CancellationToken cancellationToken)
    {
 
        if (request.personUpdateRequest is null)
        {
            throw new ArgumentNullException(nameof(request.personUpdateRequest), "Person update request cannot be null");
        }
        var person = await _context.Person!
             .FirstOrDefaultAsync(p=> p.Id == request.personId, cancellationToken);
        if (person is null)
        {
            throw new InvalidOperationException($"Person with email {request.personUpdateRequest.Email} does not exist.");
        }
       
        person.FirstName = request.personUpdateRequest.FirstName;
        person.LastName = request.personUpdateRequest.LastName;
        person.Email = request.personUpdateRequest.Email;
        person.PhoneNumber = request.personUpdateRequest.PhoneNumber;
        
        _context.Person!.Update(person);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result <= 0)
        {
            throw new InvalidOperationException("Failed to update person.");
        }
        return _mapper.Map<PersonResponse>(person);
    }
}