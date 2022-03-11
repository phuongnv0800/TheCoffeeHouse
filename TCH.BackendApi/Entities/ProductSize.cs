using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities
{
    public class ProductSize
    {
        public string ID { get; set; }
        public double Price { get; set; }
        public SizeType Size { get; set; }
        public string ProductID { get; set; }
        public Product Product { get; set; }
    }
}
