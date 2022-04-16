namespace TCH.Data.Entities;

public class StockMaterial
{
    public string BranchID { get; set; }
    public Branch Branch{ get; set; }
    public string MaterialID { get; set; }
    public Material Material { get; set; }
    public int Quantity { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Status { get; set; }
    public double PriceOfUnit { get; set; }
    public string Unit { get; set; }
    public string StandardUnit { get; set; }
}
