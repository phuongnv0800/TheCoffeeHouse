using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(x => x.ID);
            builder.HasAlternateKey(x => x.Phone);
            builder.HasAlternateKey(x=>x.MemberID);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(10);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Address).HasMaxLength(255);
            builder.Property(x => x.Email).HasMaxLength(255);
            builder.Property(x => x.Point).HasDefaultValue(0);

            builder.HasOne(x => x.MemberType).WithMany(x => x.Customers).HasForeignKey(z => z.MemberTypeID);

        }
    }
}
