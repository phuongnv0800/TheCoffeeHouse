using System.ComponentModel.DataAnnotations;

namespace TCH.BackendApi.Entities;

public class RecipeMaterial
{
    public string RecipeID { get; set; }
    public Recipe Recipe { get; set; }
    public Material Material { get; set; }
    public string MaterialID { get; set; }
    public double Weight { get; set; }
    public string Unit { get; set; }
}