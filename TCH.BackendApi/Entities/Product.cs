using TCH.BackendApi.Models.Enum;

namespace TCH.BackendApi.Entities;

public class Product
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public ProductType ProductType { get; set; } = ProductType.Drink;
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsSale { get; set; } = false;
    public double PriceSale { get; set; }
    public bool IsAvailable { get; set; }
    public string? UserCreateID { get; set; }
    public string? UserUpdateID { get; set; }
    public string? Formula { get; set; }
    public double Price { get; set; }
    public string? Description { get; set; }
    public string Unit { get; set; }
    public string LinkImage { get; set; }
    public string CategoryID { get; set; }
    public Category Category { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; }
    public ICollection<ProductInMenu> ProductInMenus{ get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<PromotionGift> PromotionGifts { get; set; }
}
