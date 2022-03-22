namespace TCH.Web.Models
{
    public class RequestLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public RequestLogin(string _userName, string _passWord)
        {
            UserName = _userName;
            Password = _passWord;
            RememberMe = false;
        }
        public RequestLogin()
        {

        }
    }
}
