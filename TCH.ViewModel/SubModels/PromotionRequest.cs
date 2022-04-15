using TCH.Utilities.Enum;

namespace TCH.ViewModel.SubModels;

public class PromotionRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string? Image { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreateDate { get; set; }
    public PromotionType TypePromotion { get; set; }
    public PromotionObject PromotionObject { get; set; }
    public int Status { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
}
