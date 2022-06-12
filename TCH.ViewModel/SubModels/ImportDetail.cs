using TCH.Utilities.Enum;

namespace TCH.ViewModel.SubModels;

public class ImportDetail
{
    public string MaterialID { get; set; }
    public string MaterialName { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime BeginDate { get; set; }
    public int Status { get; set; }
    public double PriceOfUnit { get; set; }
    public bool IsDelete { get; set; }
    public double Mass { get; set; }//khối lượng người dùng nhập
    public MeasureType MeasureType { get; set; }//loại hình tính: g, ml
    public string? Description { get; set; }
    public string MeasureID { get; set; }
}
