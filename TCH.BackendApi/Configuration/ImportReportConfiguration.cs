using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class ImportReportConfiguration : IEntityTypeConfiguration<ImportReport>
    {
        public void Configure(EntityTypeBuilder<ImportReport> builder)
        {
            builder.ToTable("ImportReports");
            builder.HasKey(x =>x.ID);
            builder.HasMany(x => x.ImportMaterials).WithOne(x => x.ImportReport).HasForeignKey(x => x.ImportID);
        }
    }
}
