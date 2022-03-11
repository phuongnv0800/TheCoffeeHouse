using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branchs");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Adderss).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.NormalizedName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Area).IsRequired().HasMaxLength(255);
            builder.Property(x => x.City).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Adderss).IsRequired().HasMaxLength(255);
            builder.Property(x => x.District).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.HasMany(x=>x.Orders).WithOne(z=>z.Branch).HasForeignKey(x=>x.BranchID);
            builder.HasMany(x => x.Promotions).WithOne(z => z.Branch).HasForeignKey(x => x.BranchID);
            builder.HasOne(x => x.Stock).WithOne(x => x.Branch).HasForeignKey<Branch>(x => x.StockID);

        }
    }
}
