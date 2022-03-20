using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class ExportMaterialConfiguration : IEntityTypeConfiguration<ExportMaterial>
    {
        public void Configure(EntityTypeBuilder<ExportMaterial> builder)
        {
            builder.ToTable("ExportMaterials");
            builder.HasKey(x => new {x.ExportID, x.MaterialID});
            builder.HasOne(x => x.Material).WithMany(x => x.ExportMaterials).HasForeignKey(x => x.ExportID);
            builder.HasOne(x => x.ExportReport).WithMany(x => x.ExportMaterials).HasForeignKey(x => x.ExportID);
        }
    }
}
