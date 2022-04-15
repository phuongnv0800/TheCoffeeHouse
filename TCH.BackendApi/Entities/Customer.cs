using System.ComponentModel.DataAnnotations;
using TCH.Utilities.Enum;

namespace TCH.BackendApi.Entities;

public class Customer
{
    public string ID { get; set; }

    public string? FullName { get; set; }

    [MaxLength(10)]
    [Required]
    [RegularExpression(@"(84|0[3|5|7|8|9])+([0-9]{8})\b",
        ErrorMessage = "Định dạng số điện thoại sai")]
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
    public ICollection<Order> Orders { get; set; }
}