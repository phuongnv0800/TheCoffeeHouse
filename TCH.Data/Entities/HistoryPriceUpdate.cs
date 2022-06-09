using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCH.Utilities.Enum;

namespace TCH.Data.Entities;

public class HistoryPriceUpdate
{
    [Key]
    public string HistoryPriceUpdateID { get; set; }
    public string? Name { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public HistoryType HistoryType { get; set; }
    public string? Description { get; set; }
    public double PriceOld { get; set; }
    public double PriceNew { get; set; }
    public string? UserCreateID { get; set; }

    [ForeignKey("ProductID")]
    public string? ProductID { get; set; }

    [ForeignKey("ToppingID")]
    public string? SizeID { get; set; }

    [ForeignKey("ToppingID")]
    public string? ToppingID { get; set; }
}
