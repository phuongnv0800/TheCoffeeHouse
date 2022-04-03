using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class LiquidationConfig : IEntityTypeConfiguration<Liquidation>
    {
        public void Configure(EntityTypeBuilder<Liquidation> builder)
        {
            builder.ToTable("LiquidationReports");
            builder.HasKey(x =>x.ID);
            builder.HasMany(x => x.LiquidationMaterials).WithOne(x => x.LiquidationReport).HasForeignKey(x => x.LiquidationID);
        }
    }
}
