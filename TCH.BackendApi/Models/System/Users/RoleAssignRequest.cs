using System;
using System.Collections.Generic;
using TCH.BackendApi.Models.Common;

namespace TCH.ViewModel.System.Users
{
    public class RoleAssignRequest
    {
        public string Id { get; set; }

        public List<SelectedItem> Roles { get; set; } = new List<SelectedItem>();
    }
}
