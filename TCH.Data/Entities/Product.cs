using System.ComponentModel.DataAnnotations;
using TCH.Utilities.Enum;

namespace TCH.Data.Entities;

public class Product
{
    [Key]
    public string ID { get; set; }
    public string Name { get; set; }
    public ProductType ProductType { get; set; } = ProductType.Drink;
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsSale { get; set; } = false;
    public double PriceSale { get; set; }
    public double Price { get; set; }
    public string? Description { get; set; }
    public string LinkImage { get; set; }
    public string CategoryID { get; set; }
    public Category Category { get; set; }
    public ICollection<ProductImage> ProductImages { get; set; }
    public ICollection<ProductInMenu> ProductInMenus{ get; set; }
    
    public ICollection<SizeInProduct> SizeInProducts { get; set; }
    
    public ICollection<ToppingInProduct> ToppingInProducts { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<PromotionGift> PromotionGifts { get; set; }
}
