using System.ComponentModel.DataAnnotations.Schema;
using TCH.Utilities.Enum;

namespace TCH.ViewModel.SubModels;

public class ImportRequest
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Reason { get; set; }
    public double Depreciation { get; set; }
    public double RecoveryValue { get; set; }
    public string Conclude { get; set; }
    public string LiquidationName { get; set; }
    public string LiquidationRole { get; set; }
    public string Supplier { get; set; }
    public string StockName { get; set; }
    public string Address { get; set; }
    public double TotalAmount { get; set; }
    public string BranchID { get; set; }

    public List<ImportDetail> ReportDetails { get; set; } = new List<ImportDetail>();
}
