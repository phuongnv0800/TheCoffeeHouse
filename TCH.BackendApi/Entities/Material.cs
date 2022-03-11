namespace TCH.BackendApi.Entities
{
    public class Material
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double PriceOfUnit { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime ExpriyDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UdpateDate { get; set; }
        public string UserCreateID { get; set; }
        public string UserUpdateID { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public string Unit { get; set; }

        public string StockID { get; set; }
        public Stock Stock { get; set; }
    }
}
