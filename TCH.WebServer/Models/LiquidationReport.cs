namespace TCH.WebServer.Models
{
    public class LiquidationReport
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Reason { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime BeginDate { get; set; }
        public string Description { get; set; }
        public double Depreciation { get; set; }
        public double LiquidationCost { get; set; }
        public string Conclude { get; set; }
        public double RecoveryValue { get; set; }
        public string LiquidationName { get; set; }
        public string LiquidationRole { get; set; }
        public ICollection<LiquidationMaterial> LiquidationMaterials { get; set; }
    }
}
