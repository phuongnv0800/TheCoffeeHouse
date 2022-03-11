using System.Collections.Generic;

namespace ServiceBFF.Models.Searchs
{
    public enum SortType
    {
        DESC = 1, ASC = 0
    }
    public class DinnerTableSearch
    {
        public string BranchID { get; set; }
        public string Name { get; set; } = "";
        public int Status { get; set; } = -1;
        public HashSet<string> AreaIDs { get; set; } = new HashSet<string>();
        public string SortColumnName { get; set; } = null;
        public SortType SortType { get; set; } = SortType.DESC;
    }
}
