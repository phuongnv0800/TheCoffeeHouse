namespace TCH.Web.Models
{
    public class ProductInMenu
    {
        public string MenuID { get; set; }
        public Menu Menu { get; set; }
        public string ProductID { get; set; }
        public Product Product { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
