using TCH.ViewModel.SubModels;

namespace TCH.ViewModel.RequestModel;

public class RoleAssignRequest
{
    public string Id { get; set; }

    public List<SelectedItem> Roles { get; set; } = new ();
}
