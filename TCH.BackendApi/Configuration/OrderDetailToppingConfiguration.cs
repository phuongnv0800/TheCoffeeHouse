using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class OrderDetailToppingConfiguration : IEntityTypeConfiguration<OrderDetailTopping>
    {
        public void Configure(EntityTypeBuilder<OrderDetailTopping> builder)
        {
            builder.ToTable("OrderDetailToppings");
            builder.HasKey(x => new { x.OrderDetailID,x.ToppingID  });
            builder.HasOne(x => x.OrderDetail).WithMany(x => x.OrderDetailToppings).HasForeignKey(x => x.OrderDetailID);
            builder.HasOne(x => x.Topping).WithMany(x => x.OrderDetailToppings).HasForeignKey(x => x.ToppingID);
        }
    }
}
