﻿namespace TCH.ViewModel.SubModels;

public class RoleAssignRequest
{
    public string Id { get; set; }

    public List<SelectedItem> Roles { get; set; } = new List<SelectedItem>();
}
