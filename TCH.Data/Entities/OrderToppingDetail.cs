namespace TCH.Data.Entities;

public class OrderToppingDetail
{
    public string ToppingID { get; set; }
    public Topping Topping { get; set; }
    public string OrderID { get; set; }
    public string ProductID { get; set; }
    public OrderDetail OrderDetail { get; set; }
    public int Quantity { get; set; }
    public string? Name{ get; set; }
    public double SubPrice{ get; set; }
}
