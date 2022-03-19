namespace TCH.BackendApi.Entities
{
    public class Topping
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public double SubPrice { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OrderDetailTopping> OrderDetailToppings { get; set; }
    }
}
