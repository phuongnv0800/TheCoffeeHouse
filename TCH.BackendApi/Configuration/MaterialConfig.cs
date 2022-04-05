using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class MaterialConfig : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x=>x.ImportMaterials).WithOne(x=>x.Material).HasForeignKey(x=>x.ImportID);
            builder.HasMany(x => x.ExportMaterials).WithOne(x => x.Material).HasForeignKey(x => x.ExportID);
            builder.HasMany(x => x.StockMaterials).WithOne(x => x.Material).HasForeignKey(x => x.StockID);
            builder.HasMany(x => x.LiquidationMaterials).WithOne(x => x.Material).HasForeignKey(x => x.LiquidationID);
        }
    }
}
