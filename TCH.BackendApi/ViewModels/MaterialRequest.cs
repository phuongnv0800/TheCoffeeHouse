namespace TCH.BackendApi.ViewModels;

public class MaterialRequest
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double PriceOfUnit { get; set; }
    public string? Description { get; set; }
    public string? Supplier { get; set; }
    public string Unit { get; set; }
    public string MaterialTypeID { get; set; }
}
