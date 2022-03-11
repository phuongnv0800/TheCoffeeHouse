﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration
{
    public class MemberTypeConfiguration : IEntityTypeConfiguration<MemberType>
    {
        public void Configure(EntityTypeBuilder<MemberType> builder)
        {
            builder.ToTable("MemberTypes");
            builder.HasKey(x=>x.ID);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x=>x.Description).IsRequired();
            builder.HasMany(x=>x.Customers).WithOne(x=>x.MemberType).HasForeignKey(z=>z.MemberTypeID);
        }
    }
}
