using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration
{
    public class UnitConfig : IEntityTypeConfiguration<Measure>
    {
        public void Configure(EntityTypeBuilder<Measure> builder)
        {
            builder.ToTable("Measures");
            builder.HasKey(x => x.ID);
            builder.HasIndex(u => u.Code).IsUnique();
            builder.HasMany(x => x.StockMaterials).WithOne(x => x.Measure).HasForeignKey(x => x.MeasureID);
            builder.HasMany(x => x.ReportDetails).WithOne(x => x.Measure).HasForeignKey(x => x.MeasureID);
        }
    }
}
