using TCH.Utilities.Enum;

namespace TCH.ViewModel.RequestModel;

public class UnitRequest
{
    public string Code { get; set; }

    public string Name { get; set; }

    public MeasureType MeasureType { get; set; }

    public double ConversionFactor { get; set; } //tỷ lệ

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string? UserCreateID { get; set; }

    public string? UserUpdateID { get; set; }
}
