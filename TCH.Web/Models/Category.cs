namespace TCH.Web.Models
{
    public class Category
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Description { get; set; } = "Mặc định";
        public ICollection<Product> Products { get; set; }
    }
}
