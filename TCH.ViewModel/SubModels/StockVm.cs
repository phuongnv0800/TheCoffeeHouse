namespace TCH.ViewModel.SubModels;

public class StockVm
{
    public string BranchName { get; set; }
    public string MaterialName { get; set; }
    public int Quantity { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Status { get; set; }
    public double PriceOfUnit { get; set; }
    public string Unit { get; set; }
    public string StandardUnit { get; set; }
    public string? Description { get; set; }
}
