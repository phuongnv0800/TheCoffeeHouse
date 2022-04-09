using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration;

public class BranchConfig : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branchs");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.Adderss).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.City).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Adderss).IsRequired().HasMaxLength(255);
        builder.Property(x => x.District).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Email).HasMaxLength(255);
        builder.HasMany(x => x.Orders).WithOne(z => z.Branch).HasForeignKey(x => x.BranchID);
        builder.HasMany(x => x.Users).WithOne(x => x.Branch).HasForeignKey(x => x.BranchID);
        builder.HasMany(x => x.Menus).WithOne(x => x.Branch).HasForeignKey(x => x.BranchID);
        builder.HasMany(x => x.Reports).WithOne(x => x.Branch).HasForeignKey(x => x.BranchID);
        builder.HasMany(x => x.StockMaterials).WithOne(x => x.Branch).HasForeignKey(x => x.BranchID);

    }
}
