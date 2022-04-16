using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.Data.Entities;

public class RecipeDetail
{
    [ForeignKey("ProductID")]
    public string ProductID { get; set; }

    public Product Product { get; set; }

    [ForeignKey("SizeID")]
    public string SizeID { get; set; }

    public Size Size { get; set; }

    [ForeignKey("MaterialID")]
    public string MaterialID { get; set; }

    public Material Material { get; set; }

    public double Weight { get; set; }

    public string Unit { get; set; }
}