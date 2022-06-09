using TCH.Utilities.Enum;
using TCH.ViewModel.SubModels;

namespace TCH.ViewModel.RequestModel;

public class OrderRequest
{
    
    public int TableNum { get; set; }

    public string Cashier { get; set; }

    public OrderType OrderType{ get; set; }

    public double ReducePromotion { get; set; }

    public double ReduceAmount { get; set; }

    public double CustomerPut { get; set; }

    public double CustomerReceive { get; set; }

    public double ShippingFee { get; set; } = 0;

    public double SubAmount { get; set; }

    public double TotalAmount { get; set; }

    public string? Description { get; set; }

    public PaymentType PaymentType { get; set; }

    public string? CustomerID { get; set; }

    public string BranchID { get; set; }

    public List<OrderItem>? OrderItems { get; set; }
}
