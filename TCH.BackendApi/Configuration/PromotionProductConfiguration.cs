using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class PromotionProductConfiguration : IEntityTypeConfiguration<PromotionProduct>
    {
        public void Configure(EntityTypeBuilder<PromotionProduct> builder)
        {
            builder.ToTable("PromotionProducts");
            builder.HasKey(x => new{ x.ProductID, x.PromotionID});
            builder.HasOne(bc => bc.Product).WithMany(b => b.PromotionProducts).HasForeignKey(bc => bc.ProductID);
            builder.HasOne(bc => bc.Promotion).WithMany(c => c.PromotionProducts).HasForeignKey(bc => bc.PromotionID);
        }
    }
}
