namespace TheCoffeeHouse.BackendApi.Entities
{
    public class Topping
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = "Mặc định";
    }
}
