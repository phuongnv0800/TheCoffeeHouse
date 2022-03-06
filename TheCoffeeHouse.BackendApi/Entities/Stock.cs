namespace TheCoffeeHouse.BackendApi.Entities
{
    public class Stock
    {
        public string ID { get; set; }
        public string StoreID { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UserCreateID { get; set; }
        public string UserUpdateID { get; set; }
        public string Description { get; set; } = "Mặc định";

    }
}
