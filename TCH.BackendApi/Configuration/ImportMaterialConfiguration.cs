using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class ImportMaterialConfiguration : IEntityTypeConfiguration<ImportMaterial>
    {
        public void Configure(EntityTypeBuilder<ImportMaterial> builder)
        {
            builder.ToTable("ImportMaterials");
            builder.HasKey(x => new { x.ImportID, x.MaterialID });
            builder.HasOne(x => x.Material).WithMany(x => x.ImportMaterials).HasForeignKey(x => x.ImportID);
            builder.HasOne(x => x.ImportReport).WithMany(x => x.ImportMaterials).HasForeignKey(x => x.ImportID);
        }
    }
}
