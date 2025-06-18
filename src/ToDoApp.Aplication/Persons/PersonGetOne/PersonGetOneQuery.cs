using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Aplication.DTOs.Person;
using ToDoApp.Infrastructure.Presistence;

namespace ToDoApp.Aplication.Persons.PersonGetOne;

public record PersonGetOneQueryRequest(
    PersonGetOneRequest personGetOneRequest
) : IRequest<PersonResponse>;

internal class PersonGetOneQueryHandler
    : IRequestHandler<PersonGetOneQueryRequest, PersonResponse>
{
    private readonly ToDoDbContext _context;
    private readonly IMapper _mapper;

    public PersonGetOneQueryHandler(ToDoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PersonResponse> Handle(PersonGetOneQueryRequest request, CancellationToken cancellationToken)
    {
    if (request.personGetOneRequest is null)
        {
            throw new ArgumentNullException(nameof(request.personGetOneRequest), "Person get one request cannot be null");
        }
        var person = await _context.Person!
            .FirstOrDefaultAsync(p => p.Id == request.personGetOneRequest.Id, cancellationToken);
        if (person is null)
        {
            throw new ArgumentNullException($"Person not found.");
        }
        return _mapper.Map<PersonResponse>(person);
    }
}