namespace TCH.BackendApi.Entities;

public class PromotionGift
{
    public string PromotionID { get; set; }
    public Promition Promotion { get; set; }
    public string ProductID { get; set; }
    public Product Product { get; set; }
    public double ReduceAmount { get; set; }
    public double Total { get; set; }
    public double ReducePercent { get; set; }
    public double Price { get; set; } = 0;
    public string? Description { get; set; }
}
