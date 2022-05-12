using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration;

public class UnitConversionConfig : IEntityTypeConfiguration<UnitConversion>
{
    public void Configure(EntityTypeBuilder<UnitConversion> builder)
    {
        builder.ToTable("UnitConversions");
        builder.HasKey(x => new { x.DestinationUnitID, x.SourceUnitID});

        builder.HasOne(x => x.DestinationUnit).WithMany(x => x.DestinationUnits).HasForeignKey(x => x.DestinationUnitID);
        builder.HasOne(x => x.SourceUnit).WithMany(x => x.SourceUnits).HasForeignKey(x => x.SourceUnitID);
    }
}
