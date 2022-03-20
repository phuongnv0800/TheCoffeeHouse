using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class LiquidationMaterialConfiguration : IEntityTypeConfiguration<LiquidationMaterial>
    {
        public void Configure(EntityTypeBuilder<LiquidationMaterial> builder)
        {
            builder.ToTable("LiquidationMaterials");
            builder.HasKey(x => new { x.LiquidationID, x.MaterialID });
            builder.HasOne(x => x.Material).WithMany(x => x.LiquidationMaterials).HasForeignKey(x => x.LiquidationID);
            builder.HasOne(x => x.LiquidationReport).WithMany(x => x.LiquidationMaterials).HasForeignKey(x => x.LiquidationID);
        }
    }
}
