using System.ComponentModel.DataAnnotations;

namespace TCH.Data.Entities;

public class PromotionGift
{
    public string PromotionID { get; set; }
    public Promotion Promotion { get; set; }
    public string ProductID { get; set; }
    public Product Product { get; set; }
    public bool IsRequired { get; set; } = false;
    public double ReduceAmount { get; set; }
    [Range(0, 1)]
    public double ReducePercent { get; set; }
    public string? Description { get; set; }
}
