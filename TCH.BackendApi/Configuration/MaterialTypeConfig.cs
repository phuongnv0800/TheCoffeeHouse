using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class MaterialTypeConfig : IEntityTypeConfiguration<MaterialType>
    {
        public void Configure(EntityTypeBuilder<MaterialType> builder)
        {
            builder.ToTable("MaterialTypes");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany(x => x.Materials).WithOne(x => x.MaterialType).HasForeignKey(z => z.MaterialTypeID);
        }
    }
}
