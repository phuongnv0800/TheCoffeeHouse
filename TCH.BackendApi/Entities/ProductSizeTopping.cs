using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.BackendApi.Entities;

public class ProductSizeTopping
{
    [Key]
    public string ID { get; set; }
    [ForeignKey("Products")]
    public string ProductID { get; set; }

    public Product Product { get; set; }

    [ForeignKey("Toppings")]
    public string? ToppingID { get; set; }

    public Topping Topping { get; set; }

    [ForeignKey("Sizes")]
    public string SizeID { get; set; }

    public Size Size { get; set; }
    public string Name { get; set; }
    public string? ToppingName { get; set; }
    public double Price { get; set; }

}
