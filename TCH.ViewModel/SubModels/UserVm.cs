using System.ComponentModel.DataAnnotations;
using TCH.Utilities.Enum;

namespace TCH.ViewModel.SubModels;

public class UserVm
{
    public string Id { get; set; }

    [Display(Name = "Tên tài khoản")]
    public string UserName { get; set; }

    [Display(Name = "Tên")]
    public string FirstName { get; set; }

    [Display(Name = "Họ")]
    public string LastName { get; set; }

    [Display(Name = "Ngày sinh")]
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public Gender Gender { get; set; }
    public string Avatar { get; set; }
    public Status Status { get; set; }

    public string Email { get; set; }

    [Display(Name = "Số điện thoại")]
    public string PhoneNumber { get; set; }
    public string BranchID { get; set; }
    public string BranchName { get; set; }
    public IList<string> Roles { get; set; }
}
