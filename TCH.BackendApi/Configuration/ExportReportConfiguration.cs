using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class ExportReportConfiguration : IEntityTypeConfiguration<ExportReport>
    {
        public void Configure(EntityTypeBuilder<ExportReport> builder)
        {
            builder.ToTable("ExportReports");
            builder.HasKey(x =>x.ID);
            builder.HasMany(x => x.ExportMaterials).WithOne(x => x.ExportReport).HasForeignKey(x => x.ExportID);
        }
    }
}
