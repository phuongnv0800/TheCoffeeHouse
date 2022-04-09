using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class ImportMaterialConfig : IEntityTypeConfiguration<ReportDetail>
    {
        public void Configure(EntityTypeBuilder<ReportDetail> builder)
        {
            builder.ToTable("ImportMaterials");
            builder.HasKey(x => new { x.ReportID, x.MaterialID });
            builder.HasOne(x => x.Material).WithMany(x => x.Re).HasForeignKey(x => x.ImportID);
            builder.HasOne(x => x.ImportReport).WithMany(x => x.ImportMaterials).HasForeignKey(x => x.ImportID);
        }
    }
}
