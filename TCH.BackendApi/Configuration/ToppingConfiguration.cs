using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration;

public class ToppingConfiguration : IEntityTypeConfiguration<Topping>
{
    public void Configure(EntityTypeBuilder<Topping> builder)
    {
        builder.ToTable("Toppings");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.SubPrice).IsRequired().HasColumnType("decimal(18,2)");
    }
}
