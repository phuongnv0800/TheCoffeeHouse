using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.ViewModels;

public class OrderList
{
    public string ID { get; set; }
    public int Quantity { get; set; }
    public double PriceProduct { get; set; }
    public double PriceSize { get; set; }
    public double SubAmount { get; set; }
    public SugarType SugarType { get; set; }
    public string? Description { get; set; }
    public string SizeID { get; set; }
    public string ProductID { get; set; }
    public List<OrderListTopping> Toppings { get; set; }
}
