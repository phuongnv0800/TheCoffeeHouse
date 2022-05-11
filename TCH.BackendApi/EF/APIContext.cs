using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.Configuration;
using TCH.Data.Entities;

namespace TCH.BackendApi.EF;

public class APIContext : IdentityDbContext<AppUser, AppRole, string>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public APIContext(DbContextOptions options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public APIContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BranchConfig());
        builder.ApplyConfiguration(new CategoryConfig());
        builder.ApplyConfiguration(new CustomerConfig());
        builder.ApplyConfiguration(new MaterialConfig());
        builder.ApplyConfiguration(new MaterialTypeConfig());
        builder.ApplyConfiguration(new MemberTypeConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new OrderDetailConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new ProductImageConfiguration());
        builder.ApplyConfiguration(new ProductInMenuConfiguration());
        builder.ApplyConfiguration(new PromotionConfiguration());
        builder.ApplyConfiguration(new PromotionGiftConfiguration());
        builder.ApplyConfiguration(new ReportDetailConfig());
        builder.ApplyConfiguration(new StockMaterialConfiguration());
        builder.ApplyConfiguration(new ToppingConfiguration());
        builder.Entity<RecipeDetail>().HasKey(x => new {x.MaterialID, x.ProductID, x.SizeID });
        builder.Entity<SizeInProduct>().HasKey(x => new {x.ProductID, x.SizeID});
        builder.Entity<ToppingInProduct>().HasKey(x => new {x.ProductID, x.ToppingID});
        base.OnModelCreating(builder);
    }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<MaterialType> MaterialTypes { get; set; }
    public DbSet<Bean> Beans { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<RecipeDetail> RecipeDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductInMenu> ProductInMenus { get; set; }
    public DbSet<SizeInProduct> SizeInProducts { get; set; }
    public DbSet<ToppingInProduct> ToppingInProducts { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<ReportDetail> ReportDetails { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<PromotionGift> PromotionGifts { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<StockMaterial> StockMaterials { get; set; }
    public DbSet<Topping> Toppings { get; set; }
    public DbSet<HistoryPriceUpdate> HistoryPriceUpdates { get; set; }
}
