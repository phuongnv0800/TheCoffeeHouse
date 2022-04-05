using System.ComponentModel.DataAnnotations;
using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.ViewModels;

public class UserUpdateRequest
{
    public string ID { get; set; }
    [Display(Name = "Tên")]
    public string FirstName { get; set; }
    [Display(Name = "Họ")]
    public string LastName { get; set; }
    [Display(Name = "Ngày sinh")]
    [DataType(DataType.Date)]
    public DateTime Dob { get; set; }
    public string Email { get; set; }
    [Display(Name = "Số điện thoại")]
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
    public IFormFile AvatarFile { get; set; }

}
