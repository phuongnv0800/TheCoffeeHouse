using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TCH.Utilities.Enum;

namespace TCH.ViewModel.SubModels;

public class RegisterRequest
{
    [Display(Name = "Tên")]
    [Required(ErrorMessage = "Tên không được để trống")]
    public string FirstName { get; set; }

    [Display(Name = "Họ")]
    [Required(ErrorMessage = "Họ không được để trống")]
    public string LastName { get; set; }

    [Display(Name = "Ngày sinh")]
    [DataType(DataType.Date)]
    [Required]
    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Email không được để trống")]
    public string Email { get; set; }

    [Display(Name = "Số điện thoại")]
    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    public string PhoneNumber { get; set; }

    public Gender Gender { get; set; } = Gender.Male;

    public string Address { get; set; }

    public IFormFile? AvatarFile { get; set; }
    
    public string branchID { get; set; }
    
    [Display(Name = "Tên tài khoản")]
    [Required(ErrorMessage = "Tên tài khoản không được để trống")]
    public string UserName { get; set; }

    [Display(Name = "Mật khẩu")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$",
     ErrorMessage = "Nhập ít nhất 8 kí tự, có ít nhất 1 chữ thường, 1 chữ hoa, 1 kí tự đặc biệt, 1 số")]
    [Required]
    public string Password { get; set; }

    [Display(Name = "Xác nhận mật khẩu")]
    [DataType(DataType.Password)]
    [Required]
    public string ConfirmPassword { get; set; }
}
