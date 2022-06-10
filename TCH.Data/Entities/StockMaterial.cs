using TCH.Utilities.Enum;

namespace TCH.Data.Entities;

public class StockMaterial
{
    public string ID { get; set; }
    public string BranchID { get; set; }
    public Branch Branch{ get; set; }
    public string MaterialID { get; set; }
    public Material Material { get; set; }
    public int Quantity { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Status { get; set; }
    public double PriceOfUnit { get; set; }
    public bool IsDelete { get; set; }
    public double Mass { get; set; }//khối lượng người dùng nhập
    public MeasureType MeasureType{ get; set; }//loại hình tính: g, ml
    public double StandardMass { get; set; }//khối lượng tiêu chuẩn
    public string? Description { get; set; }


    public string MeasureID { get; set; }
    public Measure Measure { get; set; }
}
