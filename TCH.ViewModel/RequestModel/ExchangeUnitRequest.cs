namespace TCH.ViewModel.SubModels;

public class ExchangeUnitRequest
{
    public string SourceUnitID { get; set; }//đơn vị nguồn

    public string DestinationUnitID { get; set; }//đơn vị đích
    public double ConversionFactor { get; set; } //tỷ lệ
    public string? Description { get; set; }
}
