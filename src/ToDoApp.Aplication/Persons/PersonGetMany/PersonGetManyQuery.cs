using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Aplication.Core;
using ToDoApp.Aplication.DTOs.Person;
using ToDoApp.Application.Core;
using ToDoApp.Domain;
using ToDoApp.Infrastructure.Presistence;


namespace ToDoApp.Aplication.Persons.PersonGetMany;

public class PersonGetManyQuery
{

    public record PersonGetMenyQueryRequest
        : IRequest<PageList<PersonResponse>>
    {
        public PersonGetManyRequest? personGetManyRequest { get; set; }
    }

    internal class PersonGetManyRequestHandler
        : IRequestHandler<PersonGetMenyQueryRequest, PageList<PersonResponse>>
    {
         private readonly ToDoDbContext _context;
        private readonly IMapper _mapper;

        public PersonGetManyRequestHandler(ToDoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PageList<PersonResponse>> Handle(
            PersonGetMenyQueryRequest request,
            CancellationToken cancellationToken
            )
        {
            IQueryable<Person> querable = _context.Person!
                .AsQueryable();

            var predicate = ExpressionBuilder.New<Person>();
            if (!string.IsNullOrEmpty(request.personGetManyRequest?.FirstName))
            {
                predicate = predicate.And(c =>
                   c.FirstName!.Contains(request.personGetManyRequest.FirstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.personGetManyRequest?.LastName))
            {
                predicate = predicate.And(c =>
                   c.LastName!.Contains(request.personGetManyRequest.LastName.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.personGetManyRequest?.Email))
            {
                predicate = predicate.And(c =>
                   c.Email!.Contains(request.personGetManyRequest.Email.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.personGetManyRequest?.PhoneNumber))
            {
                predicate = predicate.And(c =>
                   c.PhoneNumber!.Contains(request.personGetManyRequest.PhoneNumber.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.personGetManyRequest!.OrderBy))
            {
                Expression<Func<Person, object>>? orderBySelector = request.personGetManyRequest.OrderBy!.ToLower() switch
                {
                    "firstname" => c => c.FirstName!,
                    "lastname" => c => c.LastName!,
                    "email" => c => c.Email!,
                    "phonenumber" => c => c.PhoneNumber!,
                    _ => c => c.FirstName!
                };
                bool ascending = request.personGetManyRequest.IsAscending.HasValue
                                ? request.personGetManyRequest.IsAscending.Value
                                : true;

                querable = ascending
                    ? querable.OrderBy(orderBySelector)
                    : querable.OrderByDescending(orderBySelector);
            }

            querable = querable.Where(predicate);
            var totalCount = await querable.CountAsync(cancellationToken);
            var items = await querable
                .Skip(request.personGetManyRequest!.PageSize * (request.personGetManyRequest.PageNumber - 1))
                .Take(request.personGetManyRequest.PageSize)
                .ToListAsync(cancellationToken);

            var response = _mapper.Map<List<PersonResponse>>(items);
            var pagedResponse = new PageList<PersonResponse>(
                request.personGetManyRequest!.PageNumber,
                request.personGetManyRequest.PageSize,
                totalCount,
                response
            );
            return pagedResponse; 
        }
    }
}