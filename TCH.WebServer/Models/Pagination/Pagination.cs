namespace TCH.WebServer.Models.Pagination
{
    public class Pagination
    {
        public int pageSize { get; set; } = 10;
        public int totalPage { get; set; }
        public int PageNumber { get; set; }
    }
}
