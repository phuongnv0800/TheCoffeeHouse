using System.ComponentModel.DataAnnotations;

namespace TCH.ViewModel.SubModels;

public class PromotionList
{
    public string ProductID { get; set; }
    public bool IsRequired { get; set; } = false;
    public double ReduceAmount { get; set; }
    [Range(0, 1)]
    public double ReducePercent { get; set; }
    public string? Description { get; set; }
}
