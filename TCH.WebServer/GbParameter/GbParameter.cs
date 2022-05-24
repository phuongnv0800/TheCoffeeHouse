using System.Security.Claims;
using TCH.Data.Entities;
using TCH.ViewModel.SubModels;

namespace TCH.WebServer.GbParameter
{
    public static class GbParameter
    {
        public static bool IsLogin { get; set; }
        public static string UserId {get;set;}
        public static string Token { get; set;}
        public static Order Order { get; set; }
        public static IEnumerable<Claim> claims { get; set; }
    }
}
