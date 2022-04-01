namespace TCH.Web.Models
{
    public class Material
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double PriceOfUnit { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? UserCreateID { get; set; }
        public string? UserUpdateID { get; set; }
        public string? Description { get; set; }
        public string? Supplier { get; set; }
        public string Unit { get; set; }
        public string MaterialTypeID { get; set; }
        public MaterialType MaterialType { get; set; }
        public ICollection<StockMaterial> StockMaterials { get; set; }
        public ICollection<ExportMaterial> ExportMaterials { get; set; }
        public ICollection<ImportMaterial> ImportMaterials { get; set; }
        public ICollection<LiquidationMaterial> LiquidationMaterials { get; set; }
    }
}
