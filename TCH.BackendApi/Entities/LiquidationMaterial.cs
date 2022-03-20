namespace TCH.BackendApi.Entities
{
    public class LiquidationMaterial
    {
        public string LiquidationID { get; set; }
        public LiquidationReport LiquidationReport { get; set; }
        public string MaterialID { get; set; }
        public Material Material { get; set; }
        public int Quantity { get; set; }
        public DateTime Expriydate { get; set; }
        public DateTime BeginDate { get; set; }
        public int Status { get; set; }
        public double PriceOfUnit { get; set; }
    }
}
