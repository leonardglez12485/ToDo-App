using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Application.Core;

public class PageList<T>
{
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<T> Items { get; set; } = new List<T>();
    public PageList(int pageNumber, int pageSize, int totalCount, List<T> items)
    {
        CurrentPage = pageNumber;
        TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items;
    }

    public static async Task<PageList<T>> CreateAsync(
        IQueryable<T> source,
        int pageNumber,
        int pageSize)
    {
        var totalCount = await source.CountAsync();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PageList<T>(pageNumber, pageSize, totalCount, items);
    }
}