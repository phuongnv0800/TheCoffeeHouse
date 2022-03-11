namespace TCH.BackendApi.Models.Common
{
    public class PagedList<T>
    {
        public MetaData MetaData { get; set; }

        public List<T> Items { get; set; }

        public PagedList()
        {
        }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData()
            {
                TotalRecord = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            Items = items;
        }
    }
}
