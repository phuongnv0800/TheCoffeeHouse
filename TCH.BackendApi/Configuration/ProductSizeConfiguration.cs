using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder.ToTable("ProductSize");
            builder.HasKey(x=>x.ID);
            builder.Property(z=>z.ID).IsRequired();
            builder.Property(z=>z.Size).IsRequired();
            builder.Property(z=>z.Price).IsRequired().HasColumnType("decimal(18,2)"); 
            builder.HasOne(x=>x.Product).WithMany(x=>x.ProductSizes).HasForeignKey(x=>x.ProductID);
        }
    }
}
