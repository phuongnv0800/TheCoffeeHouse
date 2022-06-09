using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration;

public class MaterialConfig : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable("Materials");
        builder.HasKey(x => x.MaterialID);
        builder.Property(x => x.Name).IsRequired();
        builder.HasMany(x => x.StockMaterials).WithOne(x => x.Material).HasForeignKey(x => x.MaterialID);
    }
}
