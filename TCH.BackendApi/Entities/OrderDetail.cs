using System.Collections.Generic;
using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities;

public class OrderDetail
{
    //public string ID { get; set; }
    public string OrderID { get; set; }
    public Order Order { get; set; }
    public int Quantity { get; set; }
    public double PriceProduct { get; set; }
    public double SubAmount { get; set; }
    public string? Description { get; set; }
    public SugarType SugarType { get; set; }
    public double PriceSize { get; set; }
    public string SizeID { get; set; }
    public Size Size { get; set; }
    public string ProductID { get; set; }
    public Product Product { get; set; }
    public string? ToppingID1 { get; set; }
    public string? Topping1Name { get; set; }
    public double PriceToppping1 { get; set; }
    public string? ToppingID2 { get; set; }
    public string? Topping2Name { get; set; }
    public double PriceToppping2 { get; set; }
}
