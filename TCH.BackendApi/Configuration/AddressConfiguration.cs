using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.HasKey(x=>x.ID);
            builder.Property(x => x.City).HasMaxLength(255).IsRequired();
            builder.Property(x => x.District).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Address2).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Address1).HasMaxLength(255).IsRequired();
            builder.HasOne(x => x.Customer).WithMany(x => x.Addresses).HasForeignKey(x => x.CustomerID);
        }
    }
}
