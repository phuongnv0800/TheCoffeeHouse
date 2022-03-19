namespace TCH.BackendApi.Entities
{
    public class ProductSize
    {
        public string ProductID { get; set; }
        public Product Product { get; set; }
        public string SizeId { get; set; }
        public Size Size { get; set; }
    }
}
