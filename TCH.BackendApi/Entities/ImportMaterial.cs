namespace TCH.BackendApi.Entities;

public class ImportMaterial
{
    public string ImportID { get; set; }
    public Import ImportReport { get; set; }
    public string MaterialID { get; set; }
    public Material Material { get; set; }
    public int Quantity { get; set; }
    public DateTime Expriydate { get; set; }
    public DateTime BeginDate { get; set; }
    public int Status { get; set; }
    public double PriceOfUnit { get; set; }
}
