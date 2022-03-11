using System.Collections.Generic;

namespace TCH.BackendApi.Entities
{
    public class OrderDetail
    {
        public string ID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double SubAmount { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public string ProductID { get; set; }
        public Product Product { get; set; }
        public string OrderID { get; set; }
        public Order Order { get; set; }
        public virtual ICollection<OrderDetailTopping> OrderDetailToppings { get; set; }
    }
}
