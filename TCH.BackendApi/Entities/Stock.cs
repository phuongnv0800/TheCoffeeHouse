namespace TCH.BackendApi.Entities;

public class Stock
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string BranchID { get; set; }
    public Branch Branch { get; set; }
    public ICollection<StockMaterial> StockMaterials { get; set; }
}
