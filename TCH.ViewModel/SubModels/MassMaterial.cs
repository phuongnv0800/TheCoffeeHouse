using TCH.Utilities.Enum;

namespace TCH.ViewModel.SubModels;

public class MassMaterial
{
    public string MaterialID { get; set; }
    public string Name { get; set; }
    public StandardUnitType StandardUnitType{ get; set; }//loại hình tính: g, ml
    public double StandardMass { get; set; }//khối lượng tiêu chuẩn
    public string? Description { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
}