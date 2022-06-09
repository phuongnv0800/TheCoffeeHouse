namespace TCH.Data.Entities;

public class MaterialType
{
    public string MaterialTypeID { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public virtual ICollection<Material> Materials { get; set; }
}
