using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.Data.Entities;

namespace TCH.BackendApi.Configuration
{
    public class UnitConfig : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Units");
            builder.HasKey(x => x.ID);
            builder.HasIndex(u => u.Code).IsUnique();
        }
    }
}
