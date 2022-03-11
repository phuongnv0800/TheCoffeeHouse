using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.ToTable("Categories");
            builder.HasKey(x => x.ID);
            builder.HasMany(x=>x.Products).WithOne(x => x.Category).HasForeignKey(x=>x.CategoryID);
        }
    }
}
