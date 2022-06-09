using System.ComponentModel.DataAnnotations;


namespace TCH.Data.Entities;

public class Size
{
    [Key]
    public string SizeID { get; set; }
    public string Name { get; set; }
    public double SubPrice { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? UserCreateID { get; set; }
    public string? UserUpdateID { get; set; }
    public virtual ICollection<SizeInProduct> SizeInProducts { get; set; }
    public virtual ICollection<RecipeDetail> RecipeDetails { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    public virtual ICollection<HistoryPriceUpdate> HistoryPriceUpdates { get; set; }
}
