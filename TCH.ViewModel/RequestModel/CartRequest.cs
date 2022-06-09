namespace TCH.ViewModel.RequestModel;

public class CartRequest
{
    public Guid UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { set; get; }
}
