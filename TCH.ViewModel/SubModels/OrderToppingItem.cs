namespace TCH.ViewModel.SubModels;

public class OrderToppingItem
{
    public string ToppingID { get; set; }
    public int Quantity { get; set; } = 1;
    public double SubPrice { get; set; }
}
