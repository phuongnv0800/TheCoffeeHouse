using TCH.Web.Models.Enum;

namespace TCH.Web.Models
{
    public class Product
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public ProductType ProductType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? UserCreateID { get; set; }
        public string? UserUpdateID { get; set; }
        public string? Formula { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string Unit { get; set; }

        public string CategoryID { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<PromotionGift> PromotionGifts { get; set; }
    }
}
