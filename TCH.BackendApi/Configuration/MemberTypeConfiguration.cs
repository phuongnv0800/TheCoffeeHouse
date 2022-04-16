using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration;

public class MemberTypeConfiguration : IEntityTypeConfiguration<Bean>
{
    public void Configure(EntityTypeBuilder<Bean> builder)
    {
        builder.ToTable("MemberTypes");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.HasMany(x => x.Customers).WithOne(x => x.Bean).HasForeignKey(z => z.BeanID);
    }
}
