using ServiceBFF.Models.Paginations;

namespace TCH.BackendApi.Models.Searchs
{
    public enum SortType
    {
        DESC = 1, ASC = 0
    }
    public class Search : PagingParameterModel
    {
        public string? BranchID { get; set; } 
        public string? Name { get; set; }
        public bool IsPging { get; set; } = false;
        //public string? SortColumnName { get; set; }
        public SortType SortType { get; set; } = SortType.DESC;
    }
}
