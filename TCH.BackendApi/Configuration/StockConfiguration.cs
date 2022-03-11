using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");
            builder.HasKey(e => e.ID);
            builder.HasOne(x => x.Branch).WithOne(x => x.Stock).HasForeignKey<Stock>(x=>x.BranchID);
            builder.Property(e => e.Description).HasMaxLength(255);
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(255);
        }
    }
}
