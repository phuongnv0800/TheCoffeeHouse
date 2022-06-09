namespace TCH.Data.Entities;

public class Category
{
    public string CategoryID { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string Description { get; set; } = "Mặc định";
    public ICollection<Product> Products { get; set; }
}
