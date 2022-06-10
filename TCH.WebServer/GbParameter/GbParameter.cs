using System.Security.Claims;
using TCH.Data.Entities;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Pages.AdminDashBoard;

namespace TCH.WebServer.GbParameter
{
    public static class GbParameter
    {
        public static string Host = "http://tchhost.somee.com/";
        public static string BranchId { get; set; }
        public static bool IsLogin { get; set; }
        public static string UserId {get;set;}
        public static string Token { get; set;}
        public static OrderRequest Order { get; set; }
        public static ImportRequest ReportImport { get; set; }
        public static ExportRequest ReportExport { get; set; }
        public static IEnumerable<Claim> claims { get; set; }
        public static PromotionRequest Promotion { get; set; }
    }
}
