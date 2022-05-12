using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.Data.Entities;

public class UnitConversion
{
    [ForeignKey("SourceUnitID")]
    public string SourceUnitID { get; set; }//đơn vị nguồn
    public Unit SourceUnit { get; set; }

    [ForeignKey("DestinationUnitID")]
    public string DestinationUnitID { get; set; }//đơn vị đích
    public Unit DestinationUnit { get; set; }
    public double ConversionFactor { get; set; } //tỷ lệ
    public string? Description { get; set; }
}
