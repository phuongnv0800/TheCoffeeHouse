namespace TCH.WebServer.Models
{
    public class PagedList<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }

        public List<T> Items { get; set; }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalRecord = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public PagedList()
        {
        }
    }
}
