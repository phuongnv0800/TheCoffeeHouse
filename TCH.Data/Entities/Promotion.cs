using System.ComponentModel.DataAnnotations;
using TCH.Utilities.Enum;

namespace TCH.Data.Entities;

public class Promotion
{
    [Key]
    public string ID { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string? Image { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreateDate { get; set; }
    public PromotionType PromotionType { get; set; }
    public PromotionObject PromotionObject { get; set; }

    [Range(0, double.MaxValue)]
    public double ReduceAmount { get; set; }
    [Range(0, 1)]
    public double ReducePercent { get; set; }
    public int Status { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public ICollection<PromotionGift> PromotionGifts { get; set; }
}
