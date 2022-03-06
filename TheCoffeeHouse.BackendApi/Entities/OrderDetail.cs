namespace TheCoffeeHouse.BackendApi.Entities
{
    public class OrderDetail
    {
        public string ID { get; set; }
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public string ToppingName { get; set; }
    }
}
