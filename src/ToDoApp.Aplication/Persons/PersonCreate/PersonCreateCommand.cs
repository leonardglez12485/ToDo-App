using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Aplication.DTOs.Person;
using ToDoApp.Domain;
using ToDoApp.Infrastructure.Presistence;

namespace ToDoApp.Aplication.Persons.PersonCreate;

public class PersonCreateCommand
{
    public record PersonCreateCommandRequest(PersonCreateRequest personCreateRequest)
        : IRequest<PersonResponse>;

    internal class PersonCreateCommansHandler
        : IRequestHandler<PersonCreateCommandRequest, PersonResponse>
    {
        private readonly ToDoDbContext _context;
        private readonly IMapper _mapper;

        public PersonCreateCommansHandler(ToDoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PersonResponse> Handle(PersonCreateCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.personCreateRequest is null)
            {
                throw new ArgumentNullException(nameof(request.personCreateRequest), "Person create request cannot be null");
            }

            var person = await _context.Person!
                .FirstOrDefaultAsync(p => p.Email == request.personCreateRequest.Email, cancellationToken);
            if (person is not null)
            {
                throw new InvalidOperationException($"Person with email {request.personCreateRequest.Email} already exists.");
            }

            var newPerson = _mapper.Map<Person>(request.personCreateRequest);
            _context.Person!.Add(newPerson);
            var result = await _context.SaveChangesAsync(cancellationToken);
            if (result <= 0)
            {
                throw new InvalidOperationException("Failed to create person.");
            }
            return _mapper.Map<PersonResponse>(newPerson);
        }
    }
}