using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration;

public class OrderToppingDetailConfiguration : IEntityTypeConfiguration<OrderToppingDetail>
{
    public void Configure(EntityTypeBuilder<OrderToppingDetail> builder)
    {
        builder.ToTable("OrderToppingDetails");
        builder.HasKey(x => new { x.OrderID, x.ProductID, x.ToppingID });
        builder.Property(x => x.SubPrice).IsRequired().HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.OrderDetail).WithMany(x => x.OrderToppingDetails).HasForeignKey(x => new { x.ProductID, x.OrderID });
    }
}
