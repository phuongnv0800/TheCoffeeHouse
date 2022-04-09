using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(x => x.ID);
        builder.HasAlternateKey(x => x.Phone);
        builder.HasAlternateKey(x => x.MemberID);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(10);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Email).HasMaxLength(255);
        builder.Property(x => x.Point).HasDefaultValue(0);

        builder.HasOne(x => x.Bean).WithMany(x => x.Customers).HasForeignKey(z => z.BeanID);
        builder.HasMany(x => x.Orders).WithOne(x => x.Customer).HasForeignKey(z => z.CustomerID);

    }
}
