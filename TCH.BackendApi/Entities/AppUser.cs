using Microsoft.AspNetCore.Identity;
using TCH.Utilities.Enum;

namespace TCH.BackendApi.Entities;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    public Gender Gender { get; set; } = Gender.Male;

    public string? Address { get; set; }

    public string? Avatar { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime CreateDate { get; set; }
    
    public Status Status { get; set; }
    public string? BranchID { get; set; }
    
    public Branch Branch { get; set; }

    public ICollection<Order> Orders { get; set; }
}
