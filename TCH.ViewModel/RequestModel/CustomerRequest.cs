using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TCH.Utilities.Enum;

namespace TCH.ViewModel.SubModels;

public class CustomerRequest
{
    public string? FullName { get; set; }
    [MaxLength(10)]
    [Required]
    [RegularExpression(@"(84|0[3|5|7|8|9])+([0-9]{8})\b",
        ErrorMessage = "Định dạng số điện thoại sai")]
    public string Phone { get; set; }
    public string? Email { get; set; }
    [MaxLength(255)]
    public string Address { get; set; } = "Ngô Quyền, Hải Phòng";
    public Gender Gender { get; set; } = Gender.Female;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
    public IFormFile? File { get; set; }
}