namespace TheCoffeeHouse.BackendApi.Entities
{
    public class OrderDetailTopping
    {
        public string OrderDetailID { get; set; }
        public string ToppingID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

    }
}
