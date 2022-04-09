using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class ImportConfig : IEntityTypeConfiguration<Import>
    {
        public void Configure(EntityTypeBuilder<Import> builder)
        {
            builder.ToTable("ImportReports");
            builder.HasKey(x =>x.ID);
            builder.HasMany(x => x.ImportMaterials).WithOne(x => x.ImportReport).HasForeignKey(x => x.ImportID);
        }
    }
}
