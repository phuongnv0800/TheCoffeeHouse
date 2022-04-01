namespace TCH.Web.Models
{
    public class UserBranch
    {
        public string UserId { get; set; }
        public string BranchID { get; set; }
        public Branch Branch { get; set; }
    }
}
