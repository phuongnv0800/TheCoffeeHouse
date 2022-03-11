namespace TCH.BackendApi.Entities
{
    public class OrderDetailTopping
    {
        public string OrderDetailID { get; set; }
        public OrderDetail OrderDetail { get; set; }

        public string ToppingID { get; set; }
        public Topping Topping { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }

    }
}
