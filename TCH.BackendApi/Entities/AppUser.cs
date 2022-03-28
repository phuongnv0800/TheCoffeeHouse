using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities;

public class AppUser : IdentityUser
{
    public string? Code { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    public Gender Gender { get; set; } = Gender.Male;

    public string? Address { get; set; }

    public string? Avatar { get; set; }

    public DateTime? UpdateDate { get; set; }
    public DateTime CreateDate { get; set; }
    public RoleGroup RoleGroup { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<UserBranch> UserBranches { get; set; }
}
