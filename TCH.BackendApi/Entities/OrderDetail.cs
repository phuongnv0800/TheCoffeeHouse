using System.Collections.Generic;
using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities;

public class OrderDetail
{
    public string ID { get; set; }
    public int Quantity { get; set; }
    public double PriceProduct { get; set; }
    public double PriceSize { get; set; }
    public double SubAmount { get; set; }
    public string? Description { get; set; }
    public string SizeID { get; set; }
    public Size Size { get; set; }
    public string ProductID { get; set; }
    public Product Product { get; set; }
    public string OrderID { get; set; }
    public Order Order { get; set; }
    public ICollection<OrderDetailTopping> OrderDetailToppings { get; set; }
}
