using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class StockMaterialConfiguration : IEntityTypeConfiguration<StockMaterial>
    {
        public void Configure(EntityTypeBuilder<StockMaterial> builder)
        {
            builder.ToTable("StockMaterials");
            builder.HasKey(x => new { x.StockID, x.MaterialID });
            builder.HasOne(x => x.Material).WithMany(x => x.StockMaterials).HasForeignKey(x => x.StockID);
            builder.HasOne(x => x.Stock).WithMany(x => x.StockMaterials).HasForeignKey(x => x.StockID);
        }
    }
}
