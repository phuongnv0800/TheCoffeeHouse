using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration;

public class UnitConversionConfig : IEntityTypeConfiguration<UnitConversion>
{
    public void Configure(EntityTypeBuilder<UnitConversion> builder)
    {
        builder.ToTable("UnitConversions");
        builder.HasKey(x => x.UnitConversionID);

    }
}
