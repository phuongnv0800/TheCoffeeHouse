using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x=>x.Name).IsRequired();
            builder.Property(x=>x.Description).IsRequired();
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(y => y.CategoryID);
            builder.HasMany(x=>x.ProductImages).WithOne(x=>x.Product).HasForeignKey(x=>x.ProductId);
            builder.HasMany(x => x.PromotionGifts).WithOne(g => g.Product).HasForeignKey(y => y.ProductID);
            builder.HasMany(x => x.OrderDetails).WithOne(g => g.Product).HasForeignKey(y => y.ProductID);
            builder.HasMany(x => x.ProductDetails).WithOne(g => g.Product).HasForeignKey(y => y.ProductID);
        }
    }
}
