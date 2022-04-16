using Microsoft.AspNetCore.Identity;

namespace TCH.Data.Entities;

public class AppRole : IdentityRole
{
    public string? Description { get; set; }
}
