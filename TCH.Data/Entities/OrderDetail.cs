using TCH.Utilities.Enum;

namespace TCH.Data.Entities;

public class OrderDetail
{
    public string OrderID { get; set; }
    public Order Order { get; set; }
    public string ProductID { get; set; }
    public Product Product { get; set; }
    public string SizeID { get; set; }
    public Size Size { get; set; }
    public int Quantity { get; set; }
    public double PriceProduct { get; set; }
    public double SubAmount { get; set; }
    public string? Description { get; set; }
    public SugarType SugarType { get; set; }
    public IcedType IcedType { get; set; }
    public double PriceSize { get; set; }

    public virtual ICollection<OrderToppingDetail> OrderToppingDetails { get; set; }

    //public string? ToppingID1 { get; set; }
    //public string? Topping1Name { get; set; }
    //public double PriceToppping1 { get; set; }
    //public string? ToppingID2 { get; set; }
    //public string? Topping2Name { get; set; }
    //public double PriceToppping2 { get; set; }
}
