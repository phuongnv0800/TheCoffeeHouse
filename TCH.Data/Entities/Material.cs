namespace TCH.Data.Entities;

public class Material
{
    public string ID { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string? Description { get; set; }
    public string? LinkImage { get; set; }
    public string MaterialTypeID { get; set; }
    public MaterialType MaterialType { get; set; }
    public virtual ICollection<StockMaterial> StockMaterials { get; set; }
    public virtual ICollection<RecipeDetail> RecipeDetails { get; set; }
    public virtual ICollection<ReportDetail> ReportDetails { get; set; }
}
