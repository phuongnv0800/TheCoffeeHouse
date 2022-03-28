using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.BackendApi.Entities;

public class AppRole : IdentityRole
{
    public string? Description { get; set; }

    [ForeignKey("RoleGroupID")]
    public string? RoleGroupID { get; set; }
    public RoleGroup RoleGroup { get; set; }
}
