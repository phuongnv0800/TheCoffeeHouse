namespace TCH.BackendApi.Entities
{
    public class PromotionProduct
    {
        public string PromotionID { get; set; }
        public Promotion Promotion { get; set; }
        public string ProductID { get; set; }
        public Product Product { get; set; }
        public double ReduceAmount { get; set; }
        public double ReducePercent { get; set; }
        public double Total { get; set; }

    }
}
