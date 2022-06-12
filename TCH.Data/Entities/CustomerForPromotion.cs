using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.Data.Entities;

public class CustomerForPromotion
{
    [ForeignKey("Customer")]
    public string CustomerId { get; set; }
    public Customer Customer { get; set; }
    [ForeignKey("Promotion")]
    public string PromotionId { get; set; }
    public Promotion Promotion { get; set; }
    public string Code { get; set; }
    public bool Activate { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}