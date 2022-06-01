namespace TCH.ViewModel.SubModels;

public class ExportRequest
{
    public string Name { get; set; } = "";
    public string Code { get; set; }= "";
    public string Reason { get; set; }= "";
    public double Depreciation { get; set; }= 0;
    public double RecoveryValue { get; set; }= 0;
    public string Conclude { get; set; }= "";
    public string LiquidationName { get; set; }= "";
    public string LiquidationRole { get; set; }= "";
    public string Supplier { get; set; }= "";
    public string StockName { get; set; }= "";
    public string Address { get; set; }= "";
    public double TotalAmount { get; set; }= 0;
    public string BranchID { get; set; }= "";
    public List<ImportDetail> ReportDetails { get; set; } = new();
}
