namespace TCH.BackendApi.Entities;

public class ExportMaterial
{
    public string ExportID { get; set; }
    public Export ExportReport { get; set; }
    public string MaterialID { get; set; }
    public Material Material { get; set; }
    public int Quantity { get; set; }
    public DateTime Expriydate { get; set; }
    public DateTime BeginDate { get; set; }
    public int Status { get; set; }
    public double PriceOfUnit { get; set; }
}
