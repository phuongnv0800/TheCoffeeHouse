namespace TCH.WebServer.Models
{
    public class Topping
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double SubPrice { get; set; }
        public string? Description { get; set; }

        public ICollection<OrderDetailTopping> OrderDetailToppings { get; set; }
    }
}
