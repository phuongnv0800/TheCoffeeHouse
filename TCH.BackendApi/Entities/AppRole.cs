using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.BackendApi.Entities;

public class AppRole : IdentityRole
{
    public string? Description { get; set; }
}
