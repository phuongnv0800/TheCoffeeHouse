using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TCH.BackendApi.Configuration;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.EF
{
    public class APIContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public APIContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new AppRoleConfiguration());
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new BranchConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new ExportMaterialConfiguration());
            builder.ApplyConfiguration(new ExportReportConfiguration());
            builder.ApplyConfiguration(new ImportMaterialConfiguration());
            builder.ApplyConfiguration(new ImportReportConfiguration());
            builder.ApplyConfiguration(new LiquidationMaterialConfiguration());
            builder.ApplyConfiguration(new LiquidationReportConfiguration());
            builder.ApplyConfiguration(new MaterialConfiguration());
            builder.ApplyConfiguration(new MaterialTypeConfiguration());
            builder.ApplyConfiguration(new MemberTypeConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderDetailConfiguration());
            builder.ApplyConfiguration(new OrderDetailToppingConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductImageConfiguration());
            builder.ApplyConfiguration(new ProductInMenuConfiguration());
            builder.ApplyConfiguration(new PromotionConfiguration());
            builder.ApplyConfiguration(new PromotionGiftConfiguration());
            builder.ApplyConfiguration(new SizeConfiguration());
            builder.ApplyConfiguration(new StockConfiguration());
            builder.ApplyConfiguration(new StockMaterialConfiguration());
            builder.ApplyConfiguration(new ToppingConfiguration());
            builder.ApplyConfiguration(new UserBranchConfiguration());
            base.OnModelCreating(builder);
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ExportMaterial> ExportMaterials{ get; set; }
        public DbSet<ExportReport> ExportReports{ get; set; }
        public DbSet<ImportMaterial> ImportMaterials{ get; set; }
        public DbSet<ImportReport> ImportReports{ get; set; }
        public DbSet<LiquidationMaterial> LiquidationMaterials{ get; set; }
        public DbSet<LiquidationReport> LiquidationReports{ get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialType> MaterialTypes{ get; set; }
        public DbSet<MemberType> MemberTypes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetailTopping> OrderDetailToppings { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductInMenu> ProductInMenus{ get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionGift> PromotionGifts { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockMaterial> StockMaterials{ get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<UserBranch> UserBranches{ get; set; }

    }
}
