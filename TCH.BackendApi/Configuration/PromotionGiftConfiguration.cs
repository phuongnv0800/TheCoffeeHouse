using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration;

public class PromotionGiftConfiguration : IEntityTypeConfiguration<PromotionGift>
{
    public void Configure(EntityTypeBuilder<PromotionGift> builder)
    {
        builder.ToTable("PromotionGifts");
        builder.HasKey(x => new { x.ProductID, x.PromotionID });
        builder.HasOne(bc => bc.Product).WithMany(b => b.PromotionGifts).HasForeignKey(bc => bc.ProductID);
        builder.HasOne(bc => bc.Promotion).WithMany(c => c.PromotionGifts).HasForeignKey(bc => bc.PromotionID);
    }
}
