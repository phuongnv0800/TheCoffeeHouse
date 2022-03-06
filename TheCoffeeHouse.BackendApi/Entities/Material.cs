namespace TheCoffeeHouse.BackendApi.Entities
{
    public class Material
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string PriceOfUnit { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime ExpriyDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UdpateDate { get; set; }
        public string UserCreateID { get; set; }
        public string UserUpdateID { get; set; }
        public string Description { get; set; } = "Mặc định";
        public string Supplier { get; set; }
    }
}
