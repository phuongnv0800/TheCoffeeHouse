using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities;

public class Report
{
    [Key]
    public string ID { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Reason { get; set; }
    public DateTime CreateDate { get; set; }
    public ReportType ReportType { get; set; }
    public string Description { get; set; }
    public double Depreciation { get; set; }
    public double LiquidationCost { get; set; }
    public string Conclude { get; set; }
    public double RecoveryValue { get; set; }
    public string LiquidationName { get; set; }
    public string LiquidationRole { get; set; }
    public string Supplier { get; set; }
    public string StockName { get; set; }
    public string Address { get; set; }
    public double TotalAmount { get; set; }
    
    [ForeignKey("BranchID")]
    public string BranchID { get; set; }
    public Branch Branch { get; set; }
    [ForeignKey("UserCreateID")]
    public string UserCreateID { get; set; }
    public ICollection<ReportDetail> ReportDetails { get; set; }
}