﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCH.BackendApi.Entities;

namespace TCH.BackendApi.Configuration;

public class PromotionConfiguration : IEntityTypeConfiguration<Promition>
{
    public void Configure(EntityTypeBuilder<Promition> builder)
    {
        builder.ToTable("Promotions");
        builder.HasKey(x => x.ID);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Description);
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.Code).IsRequired().HasMaxLength(20);
        builder.HasMany(x => x.PromotionGifts).WithOne(x => x.Promotion).HasForeignKey(x => x.PromotionID);

    }
}
