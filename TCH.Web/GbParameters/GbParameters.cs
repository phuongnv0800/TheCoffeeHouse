using TCH.Web.Models;

namespace TCH.Web.GbParameters
{
    public static class GbParameters
    {
        public static string Host { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static ResponseLogin<string> RespondLogin { get; set; }
        public static string Role {get;set;}
    }
}
