namespace TCH.BackendApi.Entities;

public class ReportDetail
{
    public string ReportID { get; set; }
    public Report Report { get; set; }
    public string MaterialID { get; set; }
    public Material Material { get; set; }
    public int Quantity { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime BeginDate { get; set; }
    public int Status { get; set; }
    public double PriceOfUnit { get; set; }
}
