using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.BackendApi.Entities;

public class ToppingInProduct
{
    [ForeignKey("ProductID")]
    public string ProductID { get; set; }
    public Product Product { get; set; }
    [ForeignKey("ToppingID")]
    public string ToppingID { get; set; }
    public Topping Topping { get; set; }
}