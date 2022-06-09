using System.ComponentModel.DataAnnotations.Schema;

namespace TCH.Data.Entities;

public class UnitConversion
{
    public string UnitConversionID { get; set; }
    public string SourceUnitID { get; set; }//đơn vị nguồn

    public string DestinationUnitID { get; set; }//đơn vị đích
    public double ConversionFactor { get; set; } //tỷ lệ
    public string? Description { get; set; }
}
