namespace TCH.ViewModel.SubModels;

public class ImportDetail
{
    public string MaterialID { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime BeginDate { get; set; }
    public int Status { get; set; }
    public double PriceOfUnit { get; set; }
}
