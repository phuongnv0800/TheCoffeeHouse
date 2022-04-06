using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.BackendApi.Entities;

public class Recipe
{
    [Key]
    public string ID { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string Description { get; set; }
    public ICollection<RecipeMaterial> RecipeMaterials { get; set; }
    [ForeignKey(("ProductDetails"))]
    public string ProductDetailID { get; set; }
    public ProductDetail ProductDetail { get; set; }
}