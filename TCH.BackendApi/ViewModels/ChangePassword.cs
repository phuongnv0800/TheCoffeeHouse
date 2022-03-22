namespace TCH.BackendApi.ViewModels
{
    public class ChangePassword
    {
        public string UserName { get; set; }
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
