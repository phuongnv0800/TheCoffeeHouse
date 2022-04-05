using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.ID);
            builder.Property(z => z.Code).IsRequired().HasMaxLength(50);
            builder.Property(z => z.SubAmount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(z => z.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(z => z.ReduceAmount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(z => z.ReducePromotion).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(z => z.CustomerPut).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(z => z.ShippingFee).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(z => z.CustomerReceive).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasOne(x => x.Branch).WithMany(x => x.Orders).HasForeignKey(x => x.BranchID);
            builder.HasOne(x => x.Customer).WithMany(y => y.Orders).HasForeignKey(z => z.CustomerID);
        }
    }
}
