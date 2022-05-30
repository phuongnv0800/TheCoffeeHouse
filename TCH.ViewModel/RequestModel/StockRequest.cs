using TCH.Utilities.Enum;

namespace TCH.ViewModel.RequestModel;

public class StockRequest
{
    public string BranchID { get; set; }
    //public string BranchName { get; set; }
    public string MaterialID { get; set; }
    //public string MaterialName { get; set; }
    public int Quantity { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    //public int Status { get; set; }
    public double PriceOfUnit { get; set; }
    //public bool IsDelete { get; set; }
    //khối lượng người dùng nhập
    public double Mass { get; set; }
    //loại hình tính: g, ml
    public MeasureType MeasureType { get; set; }
    //khối lượng tiêu chuẩn
    //public double StandardMass { get; set; }
    public string? Description { get; set; }
    public string MeasureID { get; set; }
    //public string MeasureName { get; set; }
}
