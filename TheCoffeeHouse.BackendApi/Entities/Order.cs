namespace TheCoffeeHouse.BackendApi.Entities
{
    public class Order
    {
        public string ID { get; set; }
        public string StoreID { get; set; }
        public DateTime dateTime { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string UserCreateID { get; set; }
        public string UserUpdateID { get; set; }
    }
}
