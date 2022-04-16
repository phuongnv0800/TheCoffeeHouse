using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TCH.Utilities.Enum;

namespace TCH.ViewModel.SubModels;

public class PromotionRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public IFormFile File { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public PromotionType PromotionType { get; set; }
    public PromotionObject PromotionObject { get; set; }
    public double ReduceAmount { get; set; }
    [Range(0, 1)]
    public double ReducePercent { get; set; }
    public int Status { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public List<PromotionList> PromotionLists { get; set; }
}
