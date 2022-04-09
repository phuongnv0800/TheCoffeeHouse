using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration;

public class ProductInMenuConfiguration : IEntityTypeConfiguration<ProductInMenu>
{
    public void Configure(EntityTypeBuilder<ProductInMenu> builder)
    {
        builder.ToTable("ProductInMenus");
        builder.HasKey(x => new { x.ProductID, x.MenuID });
        builder.HasOne(x => x.Product).WithMany(x => x.ProductInMenus).HasForeignKey(x => x.ProductID);
    }
}
