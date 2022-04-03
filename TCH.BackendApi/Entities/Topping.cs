namespace TCH.BackendApi.Entities;

public class Topping
{
    public string ID { get; set; }
    public string Name { get; set; }
    public double SubPrice { get; set; }
    public string? Description { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }

    public ICollection<OrderDetailTopping> OrderDetailToppings { get; set; }
}
