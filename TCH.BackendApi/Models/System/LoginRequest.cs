using System.ComponentModel.DataAnnotations;

namespace TCH.BackendApi.Models.System
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu dài hơn 6 kí tự")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
