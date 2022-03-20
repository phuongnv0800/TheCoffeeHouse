using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.ToTable("Sizes");
            builder.HasKey(x=>x.ID);
            builder.Property(z=>z.SubPrice).IsRequired().HasColumnType("decimal(18,2)"); 
            builder.HasMany(x=>x.OrderDetails).WithOne(x=>x.Size).HasForeignKey(x=>x.SizeID);
        }
    }
}
