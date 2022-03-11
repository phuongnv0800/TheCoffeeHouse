using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.PriceOfUnit).IsRequired();
            builder.Property(x => x.Unit).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).HasDefaultValue("Mặc định");
            builder.Property(x => x.Supplier).IsRequired();
            builder.HasOne(x => x.Stock).WithMany(y => y.Materials).HasForeignKey(z => z.StockID);
        }
    }
}
