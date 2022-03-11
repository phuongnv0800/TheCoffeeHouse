namespace TCH.BackendApi.Entities
{
    public class Branch
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string District { get; set; }
        public string Adderss { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UserCreateID { get; set; }
        public string UserUpdateID { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }
        //public virtual ICollection<Stock> Stocks { get; set; }
        public string StockID { get; set; }
        public Stock Stock { get; set; }
    }
}
