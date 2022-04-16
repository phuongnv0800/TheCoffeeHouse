using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration;

public class StockMaterialConfiguration : IEntityTypeConfiguration<StockMaterial>
{
    public void Configure(EntityTypeBuilder<StockMaterial> builder)
    {
        builder.ToTable("StockMaterials");
        builder.HasKey(x => new { x.BranchID, x.MaterialID });
        builder.HasOne(x => x.Material).WithMany(x => x.StockMaterials).HasForeignKey(x => x.MaterialID);
        builder.HasOne(x => x.Branch).WithMany(x => x.StockMaterials).HasForeignKey(x => x.BranchID);
    }
}
