using System.ComponentModel.DataAnnotations;
using TCH.Utilities.Enum;

namespace TCH.Data.Entities;

public class Customer
{
    public string ID { get; set; }

    public string? FullName { get; set; }

    public string Phone { get; set; }
    public string? Email { get; set; }
    [MaxLength(255)]
    public string Address { get; set; } = "Ngô Quyền, Hải Phòng";
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime? UpdateDate { get; set; }
    public Gender Gender { get; set; } = Gender.Female;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
    public string MemberID { get; set; }
    public int Point { get; set; } = 0;
    public string? ConversionRate { get; set; }
    public string? Avatar { get; set; }

    public string BeanID { get; set; }
    public Bean Bean { get; set; }
}