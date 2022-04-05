using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.BackendApi.Entities;

public class ToppingInProduct
{
    [ForeignKey("Products")]
    public string ProductID { get; set; }
    [ForeignKey("Toppings")]
    public string ToppingID { get; set; }
    public Product Product { get; set; }
    public Topping Topping { get; set; }
}