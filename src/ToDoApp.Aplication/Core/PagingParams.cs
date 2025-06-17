namespace ToDoApp.Application.Core;

    public abstract class PagingParams
    {
        public int PageNumber { get; set; } = 1;
        private int MaxPageSize { get; set; } = 1000;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string? OrderBy { get; set; }
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public bool? IsAscending { get; set; } = true;
    }
