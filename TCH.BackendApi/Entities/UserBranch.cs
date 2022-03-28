namespace TCH.BackendApi.Entities;

public class UserBranch
{
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public string BranchID { get; set; }
    public Branch Branch { get; set; }
}
