using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities
{
    public class Product
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public ProductType ProductType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UserCreateID { get; set; }
        public string UserUpdateID { get; set; }
        public string Formula { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string CategoryID { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductSize> ProductSizes { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; }
    }
}
