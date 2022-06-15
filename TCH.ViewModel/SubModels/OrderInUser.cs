namespace TCH.ViewModel.SubModels;

public class OrderInUser
{
    public string UserName { get; set; }
    public string Cashier { get; set; }
    public int QuantityOrder { get; set; }
    public double ReducePromotion { get; set; }

    public double ReduceAmount { get; set; }

    public double CustomerPut { get; set; }

    public double CustomerReceive { get; set; }

    public double ShippingFee { get; set; } = 0;

    public double SubAmount { get; set; }

    public double TotalAmount { get; set; }
    
    public string? UserCreateID { get; set; }
}