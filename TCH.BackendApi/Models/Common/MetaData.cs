namespace TCH.BackendApi.Models.Common
{
    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
        public bool HasPrevius => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
