namespace TCH.WebServer.Models
{
    public class StockMaterial
    {

        public string StockID { get; set; }
        public Stock Stock { get; set; }
        public string MaterialID { get; set; }
        public Material Material { get; set; }
        public int Quantity { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime ExpriyDate { get; set; }
        public int Status { get; set; }
    }
}
