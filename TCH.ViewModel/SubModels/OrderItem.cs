using TCH.Utilities.Enum;

namespace TCH.ViewModel.SubModels;

public class OrderItem
{
    public int Quantity { get; set; }
    public double PriceProduct { get; set; }
    public double PriceSize { get; set; }
    public double SubAmount { get; set; }
    public SugarType SugarType { get; set; }
    public IcedType IcedType { get; set; }
    public string? Description { get; set; }
    public string SizeID { get; set; }
    public string NameProduct { get; set; }
    public string ProductID { get; set; }
    public List<OrderToppingItem> Toppings { get; set; }
}
