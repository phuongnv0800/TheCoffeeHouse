using TCH.Utilities.Paginations;

namespace TCH.Utilities.Searchs;

public enum SortType
{
    DESC = 1, ASC = 0
}
public class Search : PagingParameterModel
{
    // public string? BranchID { get; set; } 
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Name { get; set; }

    public bool IsPging { get; set; } = false;

    public SortType SortType { get; set; } = SortType.DESC;
}
