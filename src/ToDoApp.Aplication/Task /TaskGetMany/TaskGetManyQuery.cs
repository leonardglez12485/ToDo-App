using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Aplication.DTOs.Task;
using ToDoApp.Application.Task;
using ToDoApp.Domain;
using ToDoApp.Infrastructure.Presistence;


namespace ToDoApp.Application.Core;

public class TaskGestManyQuery
{
    public record TaskGetManyQueryRequest
      : IRequest<PageList<TaskResponse>>
    {
        public TaskGetManyRequest? taskGetManyRequest { get; set; }
    }

    internal class TaskGetManyQueryhandler
      : IRequestHandler<TaskGetManyQueryRequest, PageList<TaskResponse>>
    {
        private readonly ToDoDbContext _context;
        private readonly IMapper _mapper;

        public TaskGetManyQueryhandler(ToDoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PageList<TaskResponse>> Handle(TaskGetManyQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Taske> querable = _context.Taske!
                .Include(c => c.Person)
                .AsQueryable();
            var predicate = ExpressionBuilder.New<Taske>();
            if (!string.IsNullOrEmpty(request.taskGetManyRequest?.Title))
            {
                predicate = predicate.And(c => c.Title!.Contains(request.taskGetManyRequest.Title.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.taskGetManyRequest?.Description))
            {
                predicate = predicate.And(c => c.Description!.Contains(request.taskGetManyRequest.Description.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.taskGetManyRequest?.Status))
            {
                predicate = predicate.And(c => c.Status == request.taskGetManyRequest.Status);
            }
            if (!string.IsNullOrEmpty(request.taskGetManyRequest!.OrderBy))
            {
                Expression<Func<Taske, object>>? orderBySelector = request.taskGetManyRequest.OrderBy!.ToLower() switch
                {
                    "title" => c => c.Title!,
                    "description" => c => c.Description!,
                    _ => c => c.Title!
                };
                bool ascending = request.taskGetManyRequest.IsAscending.HasValue
                                ? request.taskGetManyRequest.IsAscending.Value
                                : true;

                querable = ascending
                    ? querable.OrderBy(orderBySelector)
                    : querable.OrderByDescending(orderBySelector);
            }
            querable = querable.Where(predicate);
            var totalCount = await querable.CountAsync(cancellationToken);
            var items = await querable
                .Skip(request.taskGetManyRequest!.PageSize * (request.taskGetManyRequest.PageNumber - 1))
                .Take(request.taskGetManyRequest.PageSize)
                .ToListAsync(cancellationToken);

            var response = _mapper.Map<List<TaskResponse>>(items);

            var pagedResponse = new PageList<TaskResponse>(
                totalCount,
                request.taskGetManyRequest!.PageNumber,
                request.taskGetManyRequest.PageSize,
                response
            );
            return pagedResponse;        
        }

        }
    }
