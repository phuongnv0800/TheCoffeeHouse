using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages");
        builder.HasKey(x => x.ID);
        builder.HasOne(x => x.Product).WithMany(g => g.ProductImages).HasForeignKey(x => x.ProductId);
        builder.Property(x => x.Caption).HasMaxLength(255);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.ImagePath).IsRequired();
    }

}
