using System.Data.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Aplication.DTOs.Person;
using ToDoApp.Infrastructure.Presistence;

namespace ToDoApp.Aplication.Persons.PersonDelete;

public record PersonDeleteCommandRequest(Guid Id)
    : IRequest<PersonResponse>;

internal class PersonDeleteCommandHandler
    : IRequestHandler<PersonDeleteCommandRequest, PersonResponse>
{
    private readonly ToDoDbContext _context;
    private readonly IMapper _mapper;

    public PersonDeleteCommandHandler(ToDoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PersonResponse> Handle(PersonDeleteCommandRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException("Person delete request cannot be null");
        }

        var person = await _context.Person!
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken) ?? throw new ArgumentNullException($"Person not found.");

        if (person is null)
        {
            throw new ArgumentNullException($"Person not found.");
        }
        _context.Person!.Remove(person);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0
            ? _mapper.Map<PersonResponse>(person)
            : throw new InvalidOperationException("Failed to delete person.");
    }
}