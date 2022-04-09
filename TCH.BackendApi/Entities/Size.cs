using System.ComponentModel.DataAnnotations;
using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities;

public class Size
{
    [Key]
    public string ID { get; set; }
    public string Name { get; set; }
    public double SubPrice { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public ICollection<SizeInProduct> SizeInProducts { get; set; }
    public ICollection<RecipeDetail> RecipeDetails { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
