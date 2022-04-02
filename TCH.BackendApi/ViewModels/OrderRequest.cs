using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.ViewModels;

public class OrderRequest
{
    public double Vat { get; set; }
    public OrderType OrderType { get; set; }
    public double ReducePromotion { get; set; }
    public double ReduceAmount { get; set; }
    public double CustomerPut { get; set; }
    public double CustomerReceive { get; set; }
    public double ShippingFee { get; set; }
    public PaymentType PaymentType { get; set; }
    public string? Description { get; set; }
    public string? CustomerID { get; set; }
    public string BranchID { get; set; }
    public List<OrderList> OrderItems { get; set; }
}
